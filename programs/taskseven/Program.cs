﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Taskseven
    {
        static void Main(string[] args)
        {
            string path = @"C:\task7\"; // Sets the directory path

            string[] files = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly); // Gets an array of all the files with a .txt extension in the specified directory

            using (var output = File.Create(path + "output.txt")) // Creates a new file called output.txt in the specified directory
            {
                foreach (var file in files) // Loops through each file in the files array
                {
                    using (var data = File.OpenRead(file)) // Opens the file in read mode
                    {
                        data.CopyTo(output); // Copies the contents of the file to the output file
                    }
                }

                output.Flush(); // Flushes any remaining data in the output file
                output.Close(); // Closes the output file
            }

            string[] fileNames = Directory.GetFiles(path); // Gets the names of all files in the directory

            List<int[]> roadArrays = new List<int[]>(); // Creates a list to hold arrays of integers read from the file

            foreach (string fileName in fileNames) // Loops through each file in the directory
            {
                if (fileName.Contains("output") && fileName.EndsWith(".txt")) // Checks if the file name contains "output" and ends with ".txt"
                {
                    int[] array = File.ReadAllLines(fileName) // Reads all lines from the file and convert them to an array of integers
                        .Select(line => int.Parse(line))
                        .ToArray();

                    roadArrays.Add(array); // Adds the array to the list of road arrays

                    Console.WriteLine("File Name: " + fileName); // Displays the file name

                    // Prints the array in ascending order
                    Console.WriteLine("Ascending order:");
                    QuickSort(array, 0, array.Length - 1);
                    for (int i = 0; i < array.Length; i++)
                    {
                        Console.Write(array[i] + " ");
                        if ((i + 1) % 10 == 0)
                        {
                            Console.Write("\n");
                        }
                    }

                    // Prints the array in descending order
                    Console.WriteLine("\nDescending order:");
                    QuickSort(array, 0, array.Length - 1, true);
                    for (int i = 0; i < array.Length; i++)
                    {
                        Console.Write(array[i] + " ");
                        if ((i + 1) % 10 == 0)
                        {
                            Console.Write("\n");
                        }
                    }

                    // Prints every 50th value in the array
                    Console.WriteLine();
                    Console.WriteLine("Every 50th value: ");
                    for (int i = 49; i < array.Length; i += 50)
                    {
                        Console.Write(array[i] + " ");
                    }
                    Console.WriteLine("\n");


                }
            }

            SearchValueInArray(roadArrays[0]); // Searches for a value in the first road array
            Console.ReadLine(); // Waits for user input before closing the program
        }

        // Sorts an array of integers using the quicksort algorithm
        static void QuickSort(int[] array, int left, int right, bool descending = false)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right, descending);
                QuickSort(array, left, pivot - 1, descending);
                QuickSort(array, pivot + 1, right, descending);
            }
        }

        // Partitions an array of integers for use in quicksort
        static int Partition(int[] array, int left, int right, bool descending = false)
        {
            int pivot = array[right];
            int i = left - 1;
            for (int j = left; j < right; j++)
            {
                if ((!descending && array[j] <= pivot) || (descending && array[j] >= pivot))
                {
                    i++;
                    Swap(array, i, j);
                }
            }
            Swap(array, i + 1, right);
            return i + 1;
        }

        // Swaps two elements in an array of integers
        static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        static void SearchValueInArray(int[] array)
        {
            Console.WriteLine("Enter the value to search for: "); // Prompts the user to enter the value to search for
            int value = int.Parse(Console.ReadLine()); // Reads the input value and parse it as an integer

            bool found = false; // Initialises a boolean variable to keep track of whether the value was found in the array
            List<int> indexes = new List<int>(); // Initialises a list to store the indexes of the value(s) found in the array

            for (int i = 0; i < array.Length; i++) // Loops through each element in the array
            {
                if (array[i] == value) // Checks if the current element is equal to the input value
                {
                    // If so, sets the "found" variable to true and add the index of the current element to the "indexes" list
                    found = true;
                    indexes.Add(i);
                }
            }

            // If the value was found in the array, display its index(es)
            if (found)
            {
                Console.WriteLine("Value {0} found at index(es): {1}", value, string.Join(", ", indexes));
            }

            // If the value was not found in the array, finds the nearest value and displays its index(es)
            else
            {
                int nearestValue = GetNearestValue(array, value, out List<int> nearestIndexes);
                Console.WriteLine("Value {0} not found. Nearest value {1} found at index(es): {2}", value, nearestValue, string.Join(", ", nearestIndexes));
            }
        }

        static int GetNearestValue(int[] array, int value, out List<int> indexes)
        {
            int nearestValue = 0; // Initialises a variable to store the nearest value found in the array
            int minDiff = int.MaxValue; // Initialises a variable to store the minimum difference between the input value and the elements in the array
            indexes = new List<int>(); // Initialises a list to store the indexes of the nearest value(s) found in the array

            // Loops through each element in the array
            for (int i = 0; i < array.Length; i++)
            {
                int diff = Math.Abs(array[i] - value); // Calculates the absolute difference between the current element and the input value


                if (diff < minDiff) // If the difference is less than the current minimum difference:
                {
                    minDiff = diff; // Updates the minimum difference 
                    nearestValue = array[i]; // Updates the nearest value
                    indexes.Clear(); // Clears the "indexes" list
                    indexes.Add(i); // Adds the index of the current element
                }

                else if (diff == minDiff) // If the difference is equal to the current minimum difference:
                {
                    indexes.Add(i); // Adds the index of the current element to the "indexes" list.
                }
            }

            return nearestValue; // Returns the nearest value found in the array
        }

    }

}