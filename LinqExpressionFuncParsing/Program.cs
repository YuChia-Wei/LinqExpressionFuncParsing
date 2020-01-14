using System;
using System.Linq.Expressions;

namespace LinqExpressionFuncParsing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Expression<Func<int, bool>> funcExpression = num => num == 0 && num != 10 && num != 20;
            Expression<Func<string, bool>> funcExpression = num => num == "A" && num != "B" && num != "C";

            var pExpression = funcExpression.Parameters[0]; //lambda 參數
            var body = (BinaryExpression)funcExpression.Body;  //lambda 主體

            Console.WriteLine($"解析：{pExpression.Name} => {body.Left} {body.NodeType} {body.Right}");
            Console.WriteLine($"解析：{body}");
            Console.ReadLine();
        }
    }
}