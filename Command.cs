using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CuzsieBot
{
	public class Command
	{
		public async virtual Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			return Task.CompletedTask;
		}

		public async virtual Task<Task> Init()
		{
			return Task.CompletedTask;
		}

	}

	public enum ParamaterType
	{
		None,
		Int,
		Float,
		String,
		User
	}

	public class Parameter
	{
		public ParamaterType Type = ParamaterType.None;

		private object Data = null;

		public Parameter()
		{

		}

		public Parameter(int number)
		{
			Type = ParamaterType.Int;
			Data = number;
		}

		public Parameter(float number)
		{
			Type = ParamaterType.Float;
			Data = number;
		}

		public Parameter(string text)
		{
			Type = ParamaterType.String;
			Data = text;
		}

		public Parameter(SocketUser user)
		{
			Type = ParamaterType.User;
			Data = user;
		}

		public int Int
		{
			get
			{
				if (Type == ParamaterType.Float) return (int)Float;
				if (Type != ParamaterType.Int) throw new InvalidCastException();
				return (int)Data;
			}
		}

		public float Float
		{
			get
			{
				if (Type == ParamaterType.Int) return (float)Int;
				if (Type != ParamaterType.Float) throw new InvalidCastException();
				return (float)Data;
			}
		}

		public SocketUser User
		{
			get
			{
				if (Type != ParamaterType.User) throw new InvalidCastException();
				return (SocketUser)Data;
			}
		}

		public string String
		{
			get
			{
				switch(Type)
				{
					default:
						return "Undefined Conversion";
					case ParamaterType.String:
						return (string)Data;
					case ParamaterType.Int:
						return Int.ToString();
					case ParamaterType.Float:
						return Float.ToString();
					case ParamaterType.User:
						return User.Username;
				}
			}
		}


	}


}
