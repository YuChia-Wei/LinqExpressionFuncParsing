using System;
using System.Linq.Expressions;

namespace LinqExpressionFuncParsing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Expression<Func<TestClass, bool>> funcExpression =
                num => num.PropertyA == "A" && num.PropertyB != 0 && !num.PropertyC;

            var pExpression = funcExpression.Parameters[0]; //lambda 參數
            var body = (BinaryExpression)funcExpression.Body; //lambda 主體

            Console.WriteLine($"第一個右項的型別：{body.Right.Type}");

            if (body.Right is BinaryExpression first_right)
            {
                Console.WriteLine($"解析第一個右項：{pExpression.Name} => {first_right.Left} {first_right.NodeType} {first_right.Right}");
            }

            while (body.Left is BinaryExpression left)
            {
                if (body.Right is BinaryExpression right)
                {
                    if (right.Left is MemberExpression member)
                    {
                        Console.WriteLine($"解析右項成員名稱：{member.Member.Name}");
                    }

                    if (right.Right is ConstantExpression constant)
                    {
                        Console.WriteLine($"解析右項內容：{constant.Value}");
                    }

                    Console.WriteLine($"解析右項：{pExpression.Name} => {right.Left} {right.NodeType} {right.Right}");
                }

                Console.WriteLine($"完整節點：{pExpression.Name} => {body.Left} {body.NodeType} {body.Right}");
                body = left;
            }

            Console.ReadLine();
        }

        private static void BaseTypeParsing()
        {
            Expression<Func<string, bool>> funcExpression = num => num == "A" && num != "B" && num != "C";

            var pExpression = funcExpression.Parameters[0]; //lambda 參數
            var body = (BinaryExpression)funcExpression.Body; //lambda 主體

            Console.WriteLine($"解析：{pExpression.Name} => {body.Left} {body.NodeType} {body.Right}");
            Console.WriteLine($"解析：{body}");
            Console.ReadLine();
        }
    }
}