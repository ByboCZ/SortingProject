﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dlouhodobka_Sorting_Algoritms
{
    public partial class Vizualizace : Form
    {
        static Rectangle[] rectangles;
        static Vizualizace okno;
        int[] numbers;
        static int pocet = 20;
        static int delay = 100;
        static int algoritmus;
        static bool fMode;
        static int zapisy;
        static int porovnani;
        static double cas;
        static Label lbCas;
        static Label lbZapis;
        static Label lbPorovnani;
        static Stopwatch stopwatch;


        public Vizualizace(int pPrvku, int dDelay, int alg, bool fast)
        {
            InitializeComponent();
            okno = this;
            pocet = pPrvku;
            delay = dDelay;
            algoritmus = alg;
            fMode = fast;
            
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            
        }

        private void Vizualizace_Load(object sender, EventArgs e)
        {
            rectangles = new Rectangle[pocet];
            GenPole();
            //1030 -> 930 šířka jen,  50 margin
            // Generování a ukládání obdélníků do pole
            int startX = 50;
            int startY = 489 - 70;
            int width = (930) / pocet;
            if(width < 1)
            {
                width = 1;
                okno.Width = pocet + 100;
            }
            for (int i = 0; i < pocet; i++)
            {
                rectangles[i] = new Rectangle(startX + i * (width), startY - Map(numbers[i], pocet, 360), width, Map(numbers[i], pocet, 360));
            }

            lb_prvky.Text = "Počet prvků: " + pocet;
            lbCas = lb_cas;
            lbPorovnani = lb_porovnani;
            lbZapis = lb_zapisu;

            zapisy = 0;
            porovnani = 0;
            cas = 0;

            switch (algoritmus)
            {
                case 1:
                    this.Text = "Bubble Sort";
                    break;
                case 2:
                    this.Text = "Odd-Even Sort";
                    break;
                case 3:
                    this.Text = "Quick Sort";
                    break;
                case 4:
                    this.Text = "Bogo Sort";
                    break;
                case 5:
                    this.Text = "Heap Sort";
                    break;
                case 6:
                    this.Text = "Merge Sort";
                    break;
                default:
                    break;
            }
        }












        static void BubbleSortAlgorithm(int[] arr, int pocet)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        Swap(arr, j, j+1);                 
                    }
                    porovnani++;

                    if (!fMode)
                    {
                        lbPorovnani.Text = "Počet porovnání: " + porovnani;

                        cas = stopwatch.Elapsed.TotalMilliseconds;
                        lbCas.Text = "Uplynulý čas: " + cas + " ms";
                    }
                }
                if(fMode)
                {
                    okno.Refresh();
                    lbPorovnani.Text = "Počet porovnání: " + porovnani;

                    cas = stopwatch.Elapsed.TotalMilliseconds;
                    lbCas.Text = "Uplynulý čas: " + cas + " ms";
                }
            }
        }


        

        static void OddEvenSortAlgorithm(int[] arr, int pocet)
        {
            int n = arr.Length;
            bool sorted = false; // Zjistí, zda je pole již seřazené

            while (!sorted)
            {
                sorted = true;

                // Provádí se dvě fáze: lichá a sudá
                for (int i = 1; i < n - 1; i += 2) // Lichá fáze
                {
                    if (arr[i] > arr[i + 1])
                    {
                        Swap(arr, i, i + 1);
                        sorted = false;
                    }
                    porovnani++;

                    if (!fMode)
                    {
                        lbPorovnani.Text = "Počet porovnání: " + porovnani;

                        cas = stopwatch.Elapsed.TotalMilliseconds;
                        lbCas.Text = "Uplynulý čas: " + cas + " ms";
                    }
                }

                for (int i = 0; i < n - 1; i += 2) // Sudá fáze
                {
                    if (arr[i] > arr[i + 1])
                    {
                        Swap(arr, i, i + 1);
                        sorted = false;
                    }
                    porovnani++;

                    if (!fMode)
                    {
                        lbPorovnani.Text = "Počet porovnání: " + porovnani;

                        cas = stopwatch.Elapsed.TotalMilliseconds;
                        lbCas.Text = "Uplynulý čas: " + cas + " ms";
                    }
                }
                if (fMode)
                {
                    okno.Refresh();
                    lbPorovnani.Text = "Počet porovnání: " + porovnani;

                    cas = stopwatch.Elapsed.TotalMilliseconds;
                    lbCas.Text = "Uplynulý čas: " + cas + " ms";
                }
            }
        }


        private static void Quick_Sort(int[] arr, int left, int right)
        {
            // Check if there are elements to sort
            if (left < right)
            {
                // Find the pivot index
                int pivot = Partition(arr, left, right);

                // Recursively sort elements on the left and right of the pivot
                if (pivot > 1)
                {
                    Quick_Sort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    Quick_Sort(arr, pivot + 1, right);
                }
            }
        }

        // Method to partition the array
        static int Partition(int[] arr, int left, int right)
        {
            // Select the pivot element
            int pivot = arr[left];

            Color col = Color.Blue;
            if (fMode)
            {
                okno.Refresh();
            }
            // Continue until left and right pointers meet
            while (true)
            {
                using (SolidBrush brush = new SolidBrush(col))
                {
                    using (Graphics g = okno.CreateGraphics())
                    {
                        try
                        {
                            g.FillRectangle(brush, rectangles[pivot]);
                        }
                        catch
                        {

                        }
                    }
                }

                // Move left pointer until a value greater than or equal to pivot is found
                while (arr[left] < pivot)
                {
                    left++;
                    porovnani++;
                }

                // Move right pointer until a value less than or equal to pivot is found
                while (arr[right] > pivot)
                {
                    right--;
                    porovnani++;
                }

                // If left pointer is still smaller than right pointer, swap elements
                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;
                    Swap(arr, left, right);

                    porovnani++;
                    lbPorovnani.Text = "Počet porovnání: " + porovnani;

                    cas = stopwatch.Elapsed.TotalMilliseconds;
                    lbCas.Text = "Uplynulý čas: " + cas + " ms";
                }
                else
                {
                    // Return the right pointer indicating the partitioning position
                    return right;
                }
            }
        }




        static void BogoSort(int[] arr)
        {
            Random random = new Random();

            while (!IsSorted(arr))
            {
                Shuffle(arr, random);

                if (fMode)
                {
                    okno.Refresh();
                    lbPorovnani.Text = "Počet porovnání: " + porovnani;

                    cas = stopwatch.Elapsed.TotalMilliseconds;
                    lbCas.Text = "Uplynulý čas: " + cas + " ms";
                }
            }
        }

        static bool IsSorted(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1])
                {
                    return false;
                }

                porovnani++;
                if (!fMode)
                {
                    lbPorovnani.Text = "Počet porovnání: " + porovnani;

                    cas = stopwatch.Elapsed.TotalMilliseconds;
                    lbCas.Text = "Uplynulý čas: " + cas + " ms";
                }
            }
            return true;
        }

        static void Shuffle(int[] arr, Random random)
        {
            int n = arr.Length;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);

                Swap(arr, k, n);
            }
        }




        public void Sort(int[] array)
        {
            int n = array.Length;

            // Build heap (rearrange array)
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(array, n, i);

                okno.Refresh();
                lbPorovnani.Text = "Počet porovnání: " + porovnani;

                cas = stopwatch.Elapsed.TotalMilliseconds;
                lbCas.Text = "Uplynulý čas: " + cas + " ms";
            }

            // One by one extract an element from heap
            for (int i = n - 1; i > 0; i--)
            {
                // Move current root to end
                Swap(array, 0, i);

                // call max heapify on the reduced heap
                Heapify(array, i, 0);

                okno.Refresh();
                lbPorovnani.Text = "Počet porovnání: " + porovnani;

                cas = stopwatch.Elapsed.TotalMilliseconds;
                lbCas.Text = "Uplynulý čas: " + cas + " ms";
            }
        }

        void Heapify(int[] array, int n, int i)
        {
            int largest = i; // Initialize largest as root
            int l = 2 * i + 1; // left = 2*i + 1
            int r = 2 * i + 2; // right = 2*i + 2

            // If left child is larger than root
            if (l < n && array[l] > array[largest])
                largest = l;

            porovnani++;

            // If right child is larger than largest so far
            if (r < n && array[r] > array[largest])
                largest = r;

            porovnani++;

            // If largest is not root
            if (largest != i)
            {
                Swap(array, i, largest);

                // Recursively heapify the affected sub-tree
                Heapify(array, n, largest);
            }

            porovnani++;
        }
















        private void btn_Ready_Click(object sender, EventArgs e)
        {
            switch (algoritmus)
            {
                case 1:
                    stopwatch = new Stopwatch();
                    stopwatch.Start();

                    BubbleSortAlgorithm(numbers, pocet);

                    stopwatch.Stop();
                    cas = stopwatch.Elapsed.TotalMilliseconds;
                    lbCas.Text = "Uplynulý čas: " + cas + " ms";

                    okno.Refresh();
                    Kontrola(numbers, pocet);
                    break;
                case 2:
                    stopwatch = new Stopwatch();
                    stopwatch.Start();

                    OddEvenSortAlgorithm(numbers, pocet);

                    stopwatch.Stop();
                    cas = stopwatch.Elapsed.TotalMilliseconds;
                    lbCas.Text = "Uplynulý čas: " + cas + " ms";

                    okno.Refresh();
                    Kontrola(numbers, pocet);
                    break;
                case 3:
                    stopwatch = new Stopwatch();
                    stopwatch.Start();

                    Quick_Sort(numbers, 0, pocet - 1);

                    stopwatch.Stop();
                    cas = stopwatch.Elapsed.TotalMilliseconds;
                    lbCas.Text = "Uplynulý čas: " + cas + " ms";

                    okno.Refresh();
                    Kontrola(numbers, pocet);
                    break;
                case 4:
                    stopwatch = new Stopwatch();
                    stopwatch.Start();

                    BogoSort(numbers);

                    stopwatch.Stop();
                    cas = stopwatch.Elapsed.TotalMilliseconds;
                    lbCas.Text = "Uplynulý čas: " + cas + " ms";

                    okno.Refresh();
                    Kontrola(numbers, pocet);
                    break;
                case 5:
                    stopwatch = new Stopwatch();
                    stopwatch.Start();

                    Sort(numbers);

                    stopwatch.Stop();
                    cas = stopwatch.Elapsed.TotalMilliseconds;
                    lbCas.Text = "Uplynulý čas: " + cas + " ms";

                    okno.Refresh();
                    Kontrola(numbers, pocet);
                    break;
                case 6:
                    stopwatch = new Stopwatch();
                    stopwatch.Start();

                    //funkce ---------------

                    stopwatch.Stop();
                    cas = stopwatch.Elapsed.TotalMilliseconds;
                    lbCas.Text = "Uplynulý čas: " + cas + " ms";

                    okno.Refresh();
                    Kontrola(numbers, pocet);
                    break;
                // Další case případně můžeš přidat další hodnoty
                default:
                    // Příkazy, které se provedou, pokud žádný case neodpovídá výrazu
                    break;
            }
        }

        // Metoda pro prohození prvků v poli
        static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;

            Color color = Color.Red;

            // Vyplnění obdélníků odpovídající barvou
            using (SolidBrush brush = new SolidBrush(color))
            {
                using (Graphics g = okno.CreateGraphics())
                {
                    g.FillRectangle(brush, rectangles[i]);
                    g.FillRectangle(brush, rectangles[j]);
                }
            }

            rectangles[i].Y = (489 - 70) - Map(array[i], pocet, 360);   //musim změnit jak height tak i Y, potřebuju dát height na 10
            rectangles[i].Height = Map(array[i], pocet, 360);

            rectangles[j].Y = (489 - 70) - Map(array[j], pocet, 360);
            rectangles[j].Height = Map(array[j], pocet, 360);

            if (!fMode)
            {
                // Přidání zpoždění (např. 100 ms)
                Thread.Sleep(delay);

                // Aktualizace okna po každém kroku
                okno.Refresh();
            }
            zapisy++;
            lbZapis.Text = "Počet zápisů: " + zapisy;
        }

        public void GenPole()
        {
            // Vytvoření pole s čísly od 1 do 30
            numbers = Enumerable.Range(1, pocet).ToArray();

            // Vytvoření instance generátoru náhodných čísel
            Random random = new Random();

            // Protiření pole a pro každý prvek provést náhodnou výměnu s jiným prvkem
            for (int i = 0; i < numbers.Length; i++)
            {
                int randomIndex = random.Next(i, numbers.Length);
                int temp = numbers[i];
                numbers[i] = numbers[randomIndex];
                numbers[randomIndex] = temp;
            }
        }

        static int Map(int value, int originalMax, int newMax)
        {
            int originalMin = 0;
            int newMin = 1;

            // Normalizace vstupního čísla na původní rozsah
            double normalizedValue = (double)(value - originalMin) / (originalMax - originalMin);

            // Mapování normalizované hodnoty na nový rozsah
            int mappedValue = (int)(normalizedValue * (newMax - newMin) + newMin);

            return mappedValue;
        }

        public static void Kontrola(int []arr, int pocet)
        {
            int n = arr.Length;

            Color barva = Color.Green;

            // Vyplnění obdélníků odpovídající barvou
            using (SolidBrush brush = new SolidBrush(barva))
            {
                using (Graphics g = okno.CreateGraphics())
                {
                    for (int i = 0; i < n; i++)
                    {
                        g.FillRectangle(brush, rectangles[i]);
                        Thread.Sleep(2000 / pocet);
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Vykreslení obdélníků
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);
            foreach (Rectangle rect in rectangles)
            {
                g.DrawRectangle(pen, rect);
            }
        }
    }
}
