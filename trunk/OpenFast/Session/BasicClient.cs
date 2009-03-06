/*

The contents of this file are subject to the Mozilla Public License
Version 1.1 (the "License"); you may not use this file except in
compliance with the License. You may obtain a copy of the License at
http://www.mozilla.org/MPL/

Software distributed under the License is distributed on an "AS IS"
basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See the
License for the specific language governing rights and limitations
under the License.

The Original Code is OpenFAST.

The Initial Developer of the Original Code is The LaSalle Technology
Group, LLC.  Portions created by Shariq Muhammad
are Copyright (C) Shariq Muhammad. All Rights Reserved.

Contributor(s): Shariq Muhammad <shariq.muhammad@gmail.com>

*/
using System;

namespace OpenFAST.Session
{
	public sealed class BasicClient : Client
	{
		public string Name
		{
			get
			{
				return name;
			}
			
		}
		public string VendorId
		{
			get
			{
				return vendorId;
			}
			
		}
		
		private string name;

        private string vendorId;
		
		public BasicClient(string clientName, string vendorId)
		{
			this.name = clientName;
			this.vendorId = vendorId;
		}
		
		public  override bool Equals(System.Object obj)
		{
			if (obj == this)
				return true;
			if (obj == null || !(obj is BasicClient))
				return false;
			return ((BasicClient) obj).name.Equals(name);
		}
		
		public override int GetHashCode()
		{
			return name.GetHashCode();
		}
	}
}