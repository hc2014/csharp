# Task子任务

 

### 分离子任务

尽管子任务是由父任务创建的，但在默认情况下，它独立于父任务。 在以下示例中，父任务创建了一个简单的子任务。 如果多次运行该示例的代码，你可能会注意到该示例的输出与所演示的输出不同，并且该输出可能在每次运行代码时，会发生更改。 发生这种情况的原因是父任务和子任务彼此独立执行；子任务是一个分离任务。 该示例仅等待父任务完成，并且子任务在控制台应用终止之前，可能无法执行或完成。 

```c#
class Program
{
    static void Main(string[] args)
    {
        var parent = Task.Factory.StartNew(() => {
            Console.WriteLine("Outer task executing.");

            var child = Task.Factory.StartNew(() => {
                Console.WriteLine("Nested task starting.");
                Thread.SpinWait(500000);
                Console.WriteLine("Nested task completing.");
            });
        });

        parent.Wait();
        Console.WriteLine("Outer has completed.");

        Console.ReadLine();
    }

}

//Outer task executing.
//Outer has completed.
//Nested task starting.
//Nested task completing.
```

如果该子任务由 [Task.Result](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task-1.result) 对象，而不是 Task[Task.Result](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task-1.result) 对象表示，则你可以通过访问子任务的 [Task.Result](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task-1.result) 属性，确保父任务将等待子任务完成，即使该子任务是一个分离子任务。 [Result](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task-1.result) 属性在其任务完成前会进行阻止，如以下示例所示 .

```c#
using System;
using System.Threading;
using System.Threading.Tasks;

class Example
{
   static void Main()
   {
      var outer = Task<int>.Factory.StartNew(() => {
            Console.WriteLine("Outer task executing.");

            var nested = Task<int>.Factory.StartNew(() => {
                  Console.WriteLine("Nested task starting.");
                  Thread.SpinWait(5000000);
                  Console.WriteLine("Nested task completing.");
                  return 42;
            });

            // Parent will wait for this detached child.
            return nested.Result;
      });

      Console.WriteLine("Outer has returned {0}.", outer.Result);
   }
}
// The example displays the following output:
//       Outer task executing.
//       Nested task starting.
//       Nested task completing.
//       Outer has returned 42.
```





### 附加子任务

 不同于分离子任务，附加子任务与父任务紧密同步。 可以通过使用任务创建语句中的 [TaskCreationOptions.AttachedToParent](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.taskcreationoptions#System_Threading_Tasks_TaskCreationOptions_AttachedToParent) 选项，将之前示例中的分离子任务更改为附加子任务，如以下示例中所示。 在此代码中，附加子任务会在父任务之前完成。 因此，每次运行代码时，该示例的输出都是相同的。 

```c#
using System;
using System.Threading;
using System.Threading.Tasks;

public class Example
{
   public static void Main()
   {
      var parent = Task.Factory.StartNew(() => {
            Console.WriteLine("Parent task executing.");
            var child = Task.Factory.StartNew(() => {
                  Console.WriteLine("Attached child starting.");
                  Thread.SpinWait(5000000);
                  Console.WriteLine("Attached child completing.");
            }, TaskCreationOptions.AttachedToParent);
      });
      parent.Wait();
      Console.WriteLine("Parent has completed.");
   }
}
// The example displays the following output:
//       Parent task executing.
//       Attached child starting.
//       Attached child completing.
//       Parent has completed.
```



 但是，子任务仅在其父任务不会阻止附加子任务时，才可以附加到其父任务。 通过在父任务类构造函数中指定 [TaskCreationOptions.DenyChildAttach](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.taskcreationoptions#System_Threading_Tasks_TaskCreationOptions_DenyChildAttach)选项或 [TaskFactory.StartNew](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.taskfactory.startnew) 方法，父任务可以显式阻止子任务附加到其中。 如果父任务是通过调用 [Task.Run](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task.run) 方法而创建的，则可以隐式阻止子任务附加到其中。 下面的示例阐释了这一点。 这与上述示例相同，除了该父任务是通过调用 [Task.Run(Action)](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task.run#System_Threading_Tasks_Task_Run_System_Action_) 方法，而不是 [TaskFactory.StartNew(Action)](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.taskfactory.startnew#System_Threading_Tasks_TaskFactory_StartNew_System_Action_) 方法创建的。 因为子任务不能附加到其父任务，则该示例的输出是不可预知的。 因为 [Task.Run](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task.run) 重载的默认任务创建选项包括 [TaskCreationOptions.DenyChildAttach](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.taskcreationoptions#System_Threading_Tasks_TaskCreationOptions_DenyChildAttach)，所以本示例在功能上等效于“分离子任务”部分中的第一个示例。 

```c#
using System;
using System.Threading;
using System.Threading.Tasks;

public class Example
{
   public static void Main()
   {
      var parent = Task.Run(() => {
            Console.WriteLine("Parent task executing.");
            var child = Task.Factory.StartNew(() => {
                  Console.WriteLine("Child starting.");
                  Thread.SpinWait(5000000);
                  Console.WriteLine("Child completing.");
            }, TaskCreationOptions.AttachedToParent);
      });
      parent.Wait();
      Console.WriteLine("Parent has completed.");
   }
}
// The example displays output like the following:
//       Parent task executing
//       Parent has completed.
//       Attached child starting.
```



### 子任务的异常

 如果分离子任务引发了异常，则该异常必须直接在父任务中进行观察和处理，正如任何非嵌套任务一样。 如果附加子任务引发了异常，则该异常会自动传播到父任务，并返回到等待或尝试访问任务的 [Task.Result](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task-1.result) 属性的线程。 因此，通过使用附加子任务，可以一次性处理调用线程上对 [Task.Wait](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task.wait) 的调用中的所有异常。 有关详细信息，请参阅[异常处理](https://docs.microsoft.com/zh-cn/dotnet/standard/parallel-programming/exception-handling-task-parallel-library)。 



### 取消和子任务

 任务取消需要彼此协作。 也就是说，若要取消任务，则每个附加或分离的子任务必须监视取消标记的状态。 如果想要通过使用一个取消请求来取消父任务及其所有子任务，则需要将作为参数的相同令牌传递到所有的任务，并在每个任务中提供逻辑，以对每个任务中的请求作出响应。 有关详细信息，请参阅[任务取消](https://docs.microsoft.com/zh-cn/dotnet/standard/parallel-programming/task-cancellation)和[如何：取消任务及其子级](https://docs.microsoft.com/zh-cn/dotnet/standard/parallel-programming/how-to-cancel-a-task-and-its-children)。 



#### 当父任务取消时

 如果父任务在其子任务开始前取消了自身，则子任务将永远不会开始。 如果父任务在其子任务已开始后取消了自身，则子任务将完成运行，除非它自己具有取消逻辑。 有关详细信息，请参阅[任务取消](https://docs.microsoft.com/zh-cn/dotnet/standard/parallel-programming/task-cancellation)。 

#### 当分离子任务取消时

如果分离子任务使用传递到父任务的相同标记取消自身，且父任务不会等待子任务，则不会传播异常，因为该异常将被视为良性协作取消。 此行为与任何顶级任务的行为相同。



#### 当附加子任务取消时

当附加子任务使用传递到其父任务的相同标记取消自身时，[TaskCanceledException](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.taskcanceledexception) 将传播到 [AggregateException](https://docs.microsoft.com/zh-cn/dotnet/api/system.aggregateexception) 中的联接线程。 必须等待父任务，以便你除了所有通过附加子任务的图形传播的错误异常之外，还可以处理所有良性异常。

有关详细信息，请参阅[异常处理](https://docs.microsoft.com/zh-cn/dotnet/standard/parallel-programming/exception-handling-task-parallel-library)。



#### 阻止子任务附加到其父任务

由子任务引发的未经处理的异常将传播到父任务中。 可以使用此行为，从一个根任务而无需遍历任务树来观察所有子任务异常。 但是，当父任务不需要其他代码的附件时，异常传播可能会产生问题。 例如，设想下从 [Task](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task) 对象调用第三方库组件的应用。 如果第三方库组件也创建一个 [Task](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task) 对象，并指定 [TaskCreationOptions.AttachedToParent](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.taskcreationoptions#System_Threading_Tasks_TaskCreationOptions_AttachedToParent) 以将其附加到父任务中，则子任务中出现的任何未经处理的异常将会传播到父任务。 这可能会导致主应用中出现意外行为。

若要防止子任务附加到其父任务，请在创建父任务 [Task](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task) 或 [Task](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task-1) 对象时，指定 [TaskCreationOptions.DenyChildAttach](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.taskcreationoptions#System_Threading_Tasks_TaskCreationOptions_DenyChildAttach) 选项。 当某项任务尝试附加到其父任务，且其父任务指定了 [TaskCreationOptions.DenyChildAttach](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.taskcreationoptions#System_Threading_Tasks_TaskCreationOptions_DenyChildAttach) 选项时，则子任务将不能附加到父任务，并且将像未指定 [TaskCreationOptions.AttachedToParent](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.taskcreationoptions#System_Threading_Tasks_TaskCreationOptions_AttachedToParent) 选项一样进行执行。

可能还想要防止子任务在没有及时完成时附加到其父任务。 因为父任务只有在所有子任务完成后才会完成，所以长时间运行的子任务会使整个应用执行得非常缓慢。 有关演示如何通过防止子任务附加到父任务来提升应用性能的示例，请参阅[如何：防止子任务附加到父任务](https://docs.microsoft.com/zh-cn/dotnet/standard/parallel-programming/how-to-prevent-a-child-task-from-attaching-to-its-parent)。











[msdn地址]( https://docs.microsoft.com/zh-cn/dotnet/standard/parallel-programming/attached-and-detached-child-tasks?view=netframework-4.8 )

