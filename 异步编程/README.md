在使用async跟await的时候，很多带有Async后缀的异步方法的返回值是ovid类型的，如果这个时候用await来等待这个方法的话就会报错,提示信息说void()返回类型的方法无需等待.这个时候就可以用Task.Run方法来包一层
```
 NameValueCollection postValues = new NameValueCollection();
  postValues.Add("dtList", DataTableUtil.DataTableToJson(dt));
  string response = await Task.Run(() => {
    return  this.ConneService(tradeListCheckUrl, postValues, "EditPrintExpress");
  });
  if (response == Status.Error.ToString())
  {
      // 显示异常页面
      FrmException frmException = new FrmException(AppMessage.NYSO_SYSTEM_ERROR);
      frmException.ShowDialog();
      return "";
  }
  return response;
```
