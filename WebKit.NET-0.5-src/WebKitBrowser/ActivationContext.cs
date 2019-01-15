﻿/*
 * Copyright (c) 2009, Peter Nelson (charn.opcode@gmail.com)
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 * 
 * * Redistributions of source code must retain the above copyright notice, 
 *   this list of conditions and the following disclaimer.
 * * Redistributions in binary form must reproduce the above copyright notice, 
 *   this list of conditions and the following disclaimer in the documentation 
 *   and/or other materials provided with the distribution.
 *   
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE 
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
*/

// TODO:
// possible race conditions?
// either find out if we can remove the need for this stuff altogether - 
// embedding the manifest into a client application seems to work but is
// not an ideal solution - or work out how to load it from a resource - 
// I've tried this already by getting CreateActCtx to load this assembly,
// but it can't seem to find the manifest resource.

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using WebKit;
using System.ComponentModel;

namespace WebKit
{
    /// <summary>
    /// A simple interface to the Windows Activation Context API.
    /// </summary>
    /// <remarks>
    /// Activation context switching is required here to support registration
    /// free COM interop.  Ordinarily this can be achieved by embedding an
    /// application manifest with mappings to COM objects in the assembly,
    /// however this does not work in a class library.
    /// Instead we create an activation context which explicitly loads a
    /// manifest and activate this context when we need to create a COM object.
    /// </remarks>
    internal class ActivationContext : IDisposable
    {
        // Read-only state properties
        public string ManifestFileName { get; private set; }
        public bool Activated { get; private set; }
        public bool Initialized { get; private set; }

        // Private stuff...
        private NativeMethods.ACTCTX activationContext;
        private IntPtr contextHandle = IntPtr.Zero;
        private uint lastCookie = 0;
        private bool disposed = false;

        /// <summary>
        /// Constructor for ActivationContext.
        /// </summary>
        /// <param name="ManifestFileName">Path of the manifest file to load.</param>
        public ActivationContext(string ManifestFileName)
        {
            this.ManifestFileName = ManifestFileName;
            this.Activated = false;
            this.Initialized = false;
        }

        /// <summary>
        /// Activates the activation context.
        /// </summary>
        public void Activate()
        {
            if (disposed)
                throw new ObjectDisposedException(this.ToString());
            if (!Initialized)
                throw new InvalidOperationException("ActivationContext has not been initialized");
            if (!Activated)
            {
                lastCookie = 0;
                Activated = NativeMethods.ActivateActCtx(contextHandle, out lastCookie);
                int winError = Marshal.GetLastWin32Error();

                if (!Activated)
                    throw new Win32Exception(winError, "Failed to activate activation context");
            }
        }

        /// <summary>
        /// Deactivates the activation context, activating the next one down
        /// on the 'stack'.
        /// </summary>
        public void Deactivate()
        {
            if (disposed)
                throw new ObjectDisposedException(this.ToString());
            if (!Initialized)
                throw new InvalidOperationException("ActivationContext has not been initialized");
            if (Activated)
            {
                if (!NativeMethods.DeactivateActCtx(0, lastCookie))
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to deactivate activation context");
                Activated = false;
            }
        }

        /// <summary>
        /// Initializes the activation context.
        /// </summary>
        public void Initialize()
        {
            if (disposed)
                throw new ObjectDisposedException(this.ToString());
            if (!Initialized)
            {
                activationContext = new NativeMethods.ACTCTX();
                activationContext.cbSize = Marshal.SizeOf(typeof(NativeMethods.ACTCTX));
                activationContext.lpSource = this.ManifestFileName;

                contextHandle = NativeMethods.CreateActCtx(ref activationContext);
                int winError = Marshal.GetLastWin32Error();

                Initialized = (contextHandle != (IntPtr) NativeMethods.INVALID_HANDLE_VALUE);

                if (!Initialized)
                    throw new Win32Exception(winError, "Failed to initialize activation context");
            }
        }

        #region Garbage collection stuff

        ~ActivationContext()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                NativeMethods.ReleaseActCtx(contextHandle);
                disposed = true;
            }
        }

        #endregion
    }
}
