using System;
using System.Collections.Generic;
using System.Linq;

namespace AirportProblem
{
	class Program
	{
		List<string> Airports { get; } = new List<string>()
		{
			"BGI", "CDG", "DEL", "DOH", "DSM", "EWR", "EYW", "HND", "ICN", "JFK", "LGA", "LHR", "ORD", "SAN", "SFO", "SIN", "TLV", "BUD"
		};

		List<Tuple<string, string>> Routes { get; } = new List<Tuple<string, string>>()
		{
			new Tuple<string, string>("DSM", "ORD"),
			new Tuple<string, string>("ORD", "BGI"),
			new Tuple<string, string>("BGI", "LGA"),
			new Tuple<string, string>("SIN", "CDG"),
			new Tuple<string, string>("CDG", "SIN"),
			new Tuple<string, string>("CDG", "BUD"),
			new Tuple<string, string>("DEL", "DOH"),
			new Tuple<string, string>("DEL", "CDG"),
			new Tuple<string, string>("TLV", "DEL"),
			new Tuple<string, string>("EWR", "HND"),
			new Tuple<string, string>("HND", "ICN"),
			new Tuple<string, string>("HND", "JFK"),
			new Tuple<string, string>("ICN", "JFK"),
			new Tuple<string, string>("JFK", "LGA"),
			new Tuple<string, string>("EYW", "LHR"),
			new Tuple<string, string>("LHR", "SFO"),
			new Tuple<string, string>("SFO", "SAN"),
			new Tuple<string, string>("SFO", "DSM"),
			new Tuple<string, string>("SAN", "EYW")
		};


		public FlightRoute FindShortestRoute(string startingAirport, string destinationAirport)
		{
			if (Airports.Contains(startingAirport) && Airports.Contains(destinationAirport))
			{
				var result = new List<FlightRoute>();
				
				var destinationList = Routes.Where(w => w.Item2 == destinationAirport);
				destinationList.ToList().ForEach(i => result.Add(new FlightRoute(i)));

				while (result.Where(w => !w.Complete).Count() > 0)
				{
					FlightRoute[] tResult = new FlightRoute[result.Count-1];
					result.CopyTo(tResult, 0);

					foreach (var cRoute in tResult.Where(w => !w.Complete))
					{
						var connectingFlights = Routes.Where(w => cRoute.Flights.Last().Item1 == w.Item2).ToList();

						for (var j = 0; j < connectingFlights.Count(); j++)
						{
							if (j == 1)
							{
								cRoute.Flights.Add(connectingFlights[j]);

								if (cRoute.Flights.Count(w => w.Item1 == startingAirport || w.Item2 == startingAirport) > 0)
									cRoute.Complete = true;
							}
							else
							{
								FlightRoute clone = (FlightRoute)cRoute.Clone();
								clone.Flights.Add(connectingFlights[j]);

								if (clone.Flights.Count(w => w.Item1 == startingAirport || w.Item2 == startingAirport) > 0)
									clone.Complete = true;

								result.Add(clone);
							}
						}

					}

				}

				var finalResult = result.OrderBy(o => o.Flights.Count()).Where(w => w.Complete).First();

				return finalResult;
			} 
			else
			{
				throw new Exception($"Airport {startingAirport} does not exist in listed airports!");
			}
		}

		static void Main(string[] args)
		{
			
			Console.WriteLine("Hello World!");
		}
	}
}
