﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
			//character mapping:＇０＇～４８

			//operators priority
			//+、-、*、/、（、） 、^、！
			char[][] priorities = new[]
			{
				//			+	 -	  *    /    (    )    ^    !
				new char[]{'>', '>', '<', '<', '<', '>', '<', '<'}, 
				new char[]{'>', '>', '<', '<', '<', '>', '<', '<'}, 
				new char[]{'>', '>', '>', '>', '<', '>', '<', '<'}, 
				new char[]{'>', '>', '>', '>', '<', '>', '<', '<'}, 
				new char[]{'<', '<', '<', '<', '<', '=', '<', '<'}, 
				new char[]{' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}, 
				new char[]{'>', '>', '>', '>', '<', '>', '>', '<'}, 
				new char[]{'>', '>', '>', '>', ' ', '>', '>', '>'}
			};

			Dictionary<char, Dictionary<char, char>> aaa = new Dictionary<char, Dictionary<char, char>>()
			{
				{'+', new Dictionary<char, char>(){{'+', '<'}, {'-', '<'}}}
			};
			aaa.Add('+', new Dictionary<char, char>()
			{
				{'+', '>'},
				{'-', '>'},
				{'*', '>'},
				{'/', '>'},
				{'(', '>'},
				{')', '>'},
				{'^', '>'},
				{'!', '>'},
			});
			aaa['+']['-'] = ' ';

			MyStack<float> numbers = new MyStack<float>();
			MyStack<char> operators = new MyStack<char>();

			char[] chars = expression.ToCharArray();
			int index = 0;
			bool prevIsNum = false;
			while (index < chars.Length)
			{
				if (IsDigital(chars[index]))
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
				}
				else
				{
					prevIsNum = false;
					if (index == 0)
					{
						operators.Push(chars[index]);
					}

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
								operators.Push(chars[index]);
							}
							break;
						case '<':
							operators.Push(chars[index]);
							break;
						case '=':
							operators.Pop();
							break;
					}
				}

				index ++;
			}

			if (!operators.Empty())
			{
				throw new ArgumentException("expresson is wrong");
			}

			return numbers.Pop();
		}

		public static float FactorialBasic(float a)
		{
			float result = 0;
			while (a > 0)
			{
				result *= a--;
			}

			return result;
		}

		private static bool IsDigital(char c)
		{
			if (c >= 48 && c <= 48 + 10)
			{
				return true;
			}

			return false;
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
					result = a*b;
					break;
				case '/':
					result = a/b;
					break;
				case '^':
					result = (float)Math.Pow(a, b);
					break;
			}

			return result;
		}
	}
}
