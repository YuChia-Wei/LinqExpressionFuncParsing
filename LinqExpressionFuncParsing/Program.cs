using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqExpressionFuncParsing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Expression<Func<TestClass, bool>> funcExpression =
                num => num.PropertyA == "A" && num.PropertyB != 0 && num.PropertyC;

            var pExpression = funcExpression.Parameters[0]; //lambda 參數
            var body = (BinaryExpression)funcExpression.Body; //lambda 主體

            Console.WriteLine($"解析?：{body.Right.Type}");

            if (body.Right is BinaryExpression right)
            {
                Console.WriteLine($"解析?：{pExpression.Name} => {right.Left} {right.NodeType} {right.Right}");
            }

            while (body.Left is BinaryExpression left)
            {
                Console.WriteLine($"解析：{pExpression.Name} => {body.Left} {body.NodeType} {body.Right}");
                body = left;
            }

            Console.WriteLine($"解析 1：{pExpression.Name} => {body.Left} {body.NodeType} {body.Right}");
            Console.WriteLine($"解析 2：{body}");
            Console.ReadLine();
        }

        private static void BaseTypeParsing()
        {
            Expression<Func<string, bool>> funcExpression = num => num == "A" && num != "B" && num != "C";

            var pExpression = funcExpression.Parameters[0]; //lambda 參數
            var body = (BinaryExpression) funcExpression.Body; //lambda 主體

            Console.WriteLine($"解析：{pExpression.Name} => {body.Left} {body.NodeType} {body.Right}");
            Console.WriteLine($"解析：{body}");
            Console.ReadLine();
        }
    }
}