using System;

namespace WeatherREST
{
	public class Shop
	{
		private int id;
		private string name;
		private string address;
		private string phone;
		private string distance;
		private double lng;
		private double ltd;

		public int Id {
			get {
				return id; 
			}
			set {
				id = value; 
			}
		}

		public string Name {
			get {
				return name; 
			}
			set {
				name = value; 
			}
		}

		public string Address {
			get {
				return address; 
			}
			set {
				address = value; 
			}
		}

		public string Phone {
			get {
				return phone; 
			}
			set {
				phone = value; 
			}
		}

		public string Distance {
			get {
				return distance; 
			}
			set {
				distance = value; 
			}
		}
			
		public double Lng {
			get {
				return lng; 
			}
			set {
				lng = value; 
			}
		}

		public double Ltd {
			get {
				return ltd; 
			}
			set {
				ltd = value; 
			}
		}			
	}
}

