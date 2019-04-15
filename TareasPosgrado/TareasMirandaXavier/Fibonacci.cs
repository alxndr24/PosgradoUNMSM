using System;
using System.Diagnostics;

namespace TareasMirandaXavier
{
    class Exponential
    {
        static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Exponential exponential = new Exponential();
            ExponentialExampleType exponentialExampleType = ExponentialExampleType.Fibonacci;
            switch (exponentialExampleType)
            {
                case ExponentialExampleType.Fibonacci:
                    int n = 160; //8 40 80
                    for (int i = 1; i <= n; i++)
                    {
                        long fibonacci = exponential.Fibonacci(i);
                        Console.WriteLine("fibonacci {0} = {1}", i, fibonacci);
                    }
                    break;
            }
            Console.WriteLine("Time elapsed: {0:0.00} seconds", Math.Round(stopWatch.ElapsedMilliseconds / 1000.0, 2));
            Console.ReadLine();
        }
 
        internal long Fibonacci(long n)
        {
            if (n < 0)
            {
                throw new Exception("n can not be less than zero");
            }
            if (n <= 2)
            {
                return 1;
            }

            //declaramos las variables para capturar los ultimos valores para obtener el Numero de Fibonacci
       
            long NumRetorno = 0, ValorPrevio = 0, NumPenultimo = 0; 

            //Se realiza un recorrido que hace una operacion matematica para obtener el numero de fibonacci.
            //En el desarrollo no optimo el valor se calculaba mediante un return haciendo lenta la consulta.

            for (int i = 1; i < n; i++)
            {
                NumPenultimo = NumRetorno;
                ValorPrevio = NumPenultimo;
                NumRetorno = ValorPrevio + NumRetorno;
                
            }

            return NumRetorno;
        }

    }
}
