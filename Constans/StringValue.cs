using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallPoster.Constans
{
    public class StringValue : Attribute
    {
		private string _value;

		public StringValue(string value)
		{
			_value = value;
		}

		public string Value
		{
			get { return _value; }
		}
	}
}
