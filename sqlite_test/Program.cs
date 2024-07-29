using sqlite_test;
using System.Diagnostics;


Fsq.InitSQL();
Fsq.InitSqlSugar();
Fsq._sqlSugar.Open();

Console.WriteLine("使用FreeSql开始");
//https://freesql.net/guide/freesql-provider-sqlitecore.html
for (int k = 0; k < 10; k++)
{
    // 创建 Stopwatch 实例并启动计时
    Stopwatch stopwatch = Stopwatch.StartNew();

// 执行查询
var result = await Fsq._freeSql.Queryable<CurveModel>().Where(x=>x.NUM==807).ToListAsync();
// 停止计时
    stopwatch.Stop();

// 输出执行时间
    Console.WriteLine($"第{k+1}次,执行时间: {stopwatch.ElapsedMilliseconds} ms , {stopwatch.Elapsed.TotalSeconds} s");    
}

Console.WriteLine("使用SqlSugar开始");
//https://www.donet5.com/Home/Doc?typeId=1222
for (int k = 0; k < 10; k++)
{
    // 创建 Stopwatch 实例并启动计时
    Stopwatch stopwatch = Stopwatch.StartNew();

// 执行查询
    var result = await Fsq._sqlSugar.Queryable<CurveModel>().Where(x => x.NUM == 807).ToListAsync();
// 停止计时
    stopwatch.Stop();

// 输出执行时间
    Console.WriteLine($"第{k+1}次,执行时间: {stopwatch.ElapsedMilliseconds} ms , {stopwatch.Elapsed.TotalSeconds} s");    
}