using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Implement;

namespace Core.Common
{
	public static class MathExtension
	{
		public static void ConvertDecimalToBase(ref MyStack<string> result, int num, int @base)
		{
			if (@base > 16)
			{
				throw new ArgumentException("base overstep 16");
			}

			if (result == null || !result.Empty())
			{
				throw new ArgumentException("result is null or not empty");
			}

			string[] items = new[]{"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                "A", "B", "C", "D", "E", "F"};

			while (num > 0)
			{
				result.Push(items[num % @base]);
				num /= @base;
			}
		}

		/// <summary>
		/// decimal arithmetic expression 
		/// allow operator: +、-、*、/、（、） 、^、！
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static float ArithmeticExpressionResult(string expression)
		{
			char[] filter = new[] { '+', '-', '*', '/', '(', ')', '^', '!' };
			if (expression.Any(t => !IsDecimal(t) && !filter.Contains(t)))
			{
				throw new ArgumentException("expression has character not allowed", "expression");
			}

			//character mapping:＇０＇～４８
			//operators priority
			//+、-、*、/、（、） 、^、！
			char[][] priorities = new[]
            {
				//			+	 -	  *    /    (    )    ^    !    $
				new char[]{'>', '>', '<', '<', '<', '>', '<', '<', '>'},
                new char[]{'>', '>', '<', '<', '<', '>', '<', '<', '>'},
                new char[]{'>', '>', '>', '>', '<', '>', '<', '<', '>'},
                new char[]{'>', '>', '>', '>', '<', '>', '<', '<', '>'},
                new char[]{'<', '<', '<', '<', '<', '=', '<', '<', '>'},
                new char[]{' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                new char[]{'>', '>', '>', '>', '<', '>', '>', '<', '>'},
                new char[]{'>', '>', '>', '>', ' ', '>', '>', '>', '>'},
                new char[]{'<', '<', '<', '<', '<', ' ', '<', '<', '='}
            };

			//Dictionary<char, Dictionary<char, char>> aaa = new Dictionary<char, Dictionary<char, char>>()
			//{
			//	{'+', new Dictionary<char, char>(){{'+', '<'}, {'-', '<'}}}
			//};
			//aaa.Add('+', new Dictionary<char, char>()
			//{
			//	{'+', '>'},
			//	{'-', '>'},
			//	{'*', '>'},
			//	{'/', '>'},
			//	{'(', '>'},
			//	{')', '>'},
			//	{'^', '>'},
			//	{'!', '>'},
			//});
			//aaa['+']['-'] = ' ';

			MyStack<float> numbers = new MyStack<float>();
			MyStack<char> operators = new MyStack<char>();
			operators.Push('$');

			char[] chars = (expression + "$").ToCharArray();
			int index = 0;
			bool prevIsNum = false;
			while (index < chars.Length)
			{
				if (IsDecimal(chars[index]))
				{
					if (prevIsNum)
					{
						numbers.Push(numbers.Pop() * 10 + ((int)chars[index] - 48));
					}
					else
					{
						numbers.Push((int)chars[index] - 48);
					}
					prevIsNum = true;
					index++;
				}
				else
				{
					prevIsNum = false;

					char priority = priorities[GetOperatorOption(operators.Top())][GetOperatorOption(chars[index])];
					switch (priority)
					{
						case '>':
							char @operator = operators.Pop();
							if (@operator == '!')
							{
								numbers.Push(FactorialBasic(numbers.Pop()));
							}
							else
							{
								float num2 = numbers.Pop();
								float num1 = numbers.Pop();
								numbers.Push(Calculate(num1, @operator, num2));
							}
							break;
						case '<':
							operators.Push(chars[index++]);
							break;
						case '=':
							operators.Pop();
							index++;
							break;
						default:
							throw new ArgumentException("expresson is wrong");
					}
				}
			}

			return numbers.Pop();
		}

		/// <summary>
		/// transform arithmetic expression to Reverse Polish Notation
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static string GetRpnExpression(string expression)
		{
			char[] filter = new[] { '+', '-', '*', '/', '(', ')', '^', '!' };
			if (expression.Any(t => !IsDecimal(t) && !filter.Contains(t)))
			{
				throw new ArgumentException("expression has character not allowed", "expression");
			}

			//character mapping:＇０＇～４８
			//operators priority
			//+、-、*、/、（、） 、^、！
			char[][] priorities = new[]
            {
				//			+	 -	  *    /    (    )    ^    !    $
				new char[]{'>', '>', '<', '<', '<', '>', '<', '<', '>'},
                new char[]{'>', '>', '<', '<', '<', '>', '<', '<', '>'},
                new char[]{'>', '>', '>', '>', '<', '>', '<', '<', '>'},
                new char[]{'>', '>', '>', '>', '<', '>', '<', '<', '>'},
                new char[]{'<', '<', '<', '<', '<', '=', '<', '<', '>'},
                new char[]{' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                new char[]{'>', '>', '>', '>', '<', '>', '>', '<', '>'},
                new char[]{'>', '>', '>', '>', ' ', '>', '>', '>', '>'},
                new char[]{'<', '<', '<', '<', '<', ' ', '<', '<', '='}
            };

			MyStack<float> numbers = new MyStack<float>();
			MyStack<char> operators = new MyStack<char>();
			MyStack<string> rpns = new MyStack<string>();
			operators.Push('$');

			char[] chars = (expression + "$").ToCharArray();
			int index = 0;
			bool prevIsNum = false;
			while (index < chars.Length)
			{
				if (IsDecimal(chars[index]))
				{
					if (prevIsNum)
					{
						numbers.Push(numbers.Pop() * 10 + ((int)chars[index] - 48));
						rpns.Pop();
					}
					else
					{
						numbers.Push((int)chars[index] - 48);
					}
					prevIsNum = true;
					index++;
					rpns.Push(numbers.Top().ToString(CultureInfo.InvariantCulture));
				}
				else
				{
					prevIsNum = false;

					char priority = priorities[GetOperatorOption(operators.Top())][GetOperatorOption(chars[index])];
					switch (priority)
					{
						case '>':
							char @operator = operators.Pop();
							rpns.Push(@operator.ToString());
							break;
						case '<':
							operators.Push(chars[index++]);
							break;
						case '=':
							operators.Pop();
							index++;
							break;
						default:
							throw new ArgumentException("expresson is wrong");
					}
				}
			}

			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < rpns.Size(); i++)
			{
				builder.Append(rpns[i] + " ");
			}
			return builder.ToString();
		}

		/// <summary>
		/// return Reverse Polish Notation result
		/// </summary>
		/// <param name="rpn"></param>
		/// <returns></returns>
		public static float RpnExpressionResult(string rpn)
		{
			string[] array = rpn.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			MyStack<float> result = new MyStack<float>();
			foreach (var s in array)
			{
				if (IsDecimal(s))
				{
					result.Push(float.Parse(s));
				}
				else
				{
					float num1, num2;
					switch (s)
					{
						case "+":
							result.Push(result.Pop() + result.Pop());
							break;
						case "-":
							num2 = result.Pop();
							num1 = result.Pop();
							result.Push(num1 - num2);
							break;
						case "*":
							result.Push(result.Pop() * result.Pop());
							break;
						case "/":
							num2 = result.Pop();
							num1 = result.Pop();
							result.Push(num1 / num2);
							break;
						case "^":
							num2 = result.Pop();
							num1 = result.Pop();
							result.Push((float)Math.Pow(num1, num2));
							break;
						case "!":
							result.Push(FactorialBasic(result.Pop()));
							break;
						default:
							throw new ArgumentException("expresson is wrong");
					}
				}
			}

			if (result.Size() > 1)
			{
				throw new ArgumentException("the rpn expression is wrong");
			}

			return result.Pop();
		}

		public static float FactorialBasic(float a)
		{
			float result = 1;
			while (a > 0)
			{
				result *= a--;
			}

			return result;
		}

		#region Private Method
		private static bool IsDecimal(char c)
		{
			if (c >= 48 && c <= 48 + 10)
			{
				return true;
			}

			return false;
		}

		private static bool IsDecimal(string s)
		{
			return s.All(IsDecimal);
		}

		private static int GetOperatorOption(char c)
		{
			switch (c)
			{
				case '+':
					return 0;
				case '-':
					return 1;
				case '*':
					return 2;
				case '/':
					return 3;
				case '(':
					return 4;
				case ')':
					return 5;
				case '^':
					return 6;
				case '!':
					return 7;
				case '$':
					return 8;
			}

			throw new ArgumentException("expression is not be allowed");
		}

		private static float Calculate(float a, char oper, float b)
		{
			float result = 0;

			switch (oper)
			{
				case '+':
					result = a + b;
					break;
				case '-':
					result = a - b;
					break;
				case '*':
					result = a * b;
					break;
				case '/':
					result = a / b;
					break;
				case '^':
					result = (float)Math.Pow(a, b);
					break;
			}

			return result;
		}
		#endregion
	}
}
