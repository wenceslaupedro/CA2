using System.Net.Http.Json;

namespace WorkoutTracker.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5001/");

            try
            {
                Console.WriteLine("Fetching workout statistics...\n");
                var response = await client.GetFromJsonAsync<WorkoutStats>("api/Analysis/workout-stats");

                if (response != null)
                {
                    Console.WriteLine("Workout Statistics:");
                    Console.WriteLine($"Total Workouts: {response.TotalWorkouts}");
                    Console.WriteLine($"Total Different Exercises: {response.TotalExercises}");
                    Console.WriteLine($"Average Sets per Workout: {response.AverageSets:F1}");
                    Console.WriteLine($"Average Reps per Set: {response.AverageReps:F1}");
                    Console.WriteLine($"Total Volume (sets × reps × weight): {response.TotalVolume:F1}");
                    Console.WriteLine($"Most Performed Exercise: {response.MostPerformedExercise ?? "None"}");
                }
                else
                {
                    Console.WriteLine("No data received from the API.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Make sure the API is running and accessible.");
                Console.WriteLine("Try accessing the Swagger UI at http://localhost:5050/swagger");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

    public class WorkoutStats
    {
        public int TotalWorkouts { get; set; }
        public int TotalExercises { get; set; }
        public double AverageSets { get; set; }
        public double AverageReps { get; set; }
        public decimal TotalVolume { get; set; }
        public string? MostPerformedExercise { get; set; }
    }
}
