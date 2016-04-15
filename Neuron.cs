using System;
using System.Collections.Generic;
using System.Text;

namespace Neuronets
{
    /// <summary>
    /// Модель формального нейрона.
    /// </summary>
    class Neuron
    {
        #region Поля

        /// <summary>
        /// Коэффициенты весов входных сигналов данного нейрона. Изменяются в процессе обучения автоматически.
        /// </summary>
        private double[] _weights; 

        /// <summary>
        /// Входные немодифицированные сигналы.
        /// </summary>
        public double[] SignalsIn;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Инициализирует экземпляр класса Neuron с нулевыми значениями входных сигналов и случайными начальными весами.
        /// </summary>
        /// <param name="signalsCount">Число синапсов на входе нейрона.</param>
        /// <param name="ft">Тип активационной функции нейрона.</param>
        Neuron(int signalsCount, FunctionType ft)
        {
            SignalsIn = new double[signalsCount];
            _weights = new double[signalsCount];
            var r = new Random();
            
            for (int i = 0; i < SignalsIn.Length; ++i)
            {
                _weights[i] = Convert.ToDouble(r.Next(-1000, 1000)) / 10000; //веса инициализируются случайными значениями диапазона [-0.1; 0.1] //TODO: тестировать
                SignalsIn[i] = 0;
            }
            switch (ft) //инициализация активационной функции нейрона
            {
                case FunctionType.Linear:
                    _afd = LinearFunc; //линейная
                break;
                case FunctionType.Sigmoid:
                    _afd = SigmoidFunc; //лог-сигмоидальная
                    break;
                case FunctionType.HyperbolicTangent:
                    _afd = HyperbolicTangentFunc; //гиперболический тангенс
                    break;
                case FunctionType.Threshold:
                    _afd = ThresholdFunc; //пороговая
                    break;
                default:
                    throw new Exception("Неизвестный тип активационной функции");
            }
        }

        #endregion

        #region Активационные функции

        /// <summary>
        /// Лог-сигмоидальная активационная функция нейрона.
        /// </summary>
        /// <param name="a">Значение параметра функции.</param>
        /// <returns>Значение активационной функции в диапазоне [0; 1].</returns>
        private double SigmoidFunc(double a)
        {
            return 1.0/(1 + Math.Exp(-1*a*Sum()));
        }

        /// <summary>
        /// Линейная функция вида f(S) = aS.
        /// </summary>
        /// <param name="a">Значение линейного коэффициента.</param>
        /// <returns>Значение активационной функции в диапазоне [-∞; +∞].</returns>
        private double LinearFunc(double a)
        {
            return Sum() * a;
        }

        /// <summary>
        /// Пороговая активационная функция нейрона.
        /// </summary>
        /// <param name="d">Значение порога функции.</param>
        /// <returns>0 или 1.</returns>
        private double ThresholdFunc(double d)
        {
            return Sum() > d ? 1 : 0;
        }

        /// <summary>
        /// Гиперболический тангенс, активационная функция нейрона.
        /// </summary>
        /// <param name="a">Значение параметра функции.</param>
        /// <returns>Значение активационной функции в диапазоне [-1; 1].</returns>
        private double HyperbolicTangentFunc(double a)
        {
            double s = Sum();
            return (Math.Exp(a*s) - Math.Exp(-1*a*s)) / (Math.Exp(a*s) + Math.Exp(-1*a*s));
        }

        #endregion

        #region  Делегаты

        /// <summary>
        /// Делегат для инкапсуляции активационной функции.
        /// </summary>
        private delegate double ActivationFuncDelegate(double arg);

        /// <summary>
        /// Экземпляр делегата ActivationFuncDelegate для вызова нужной активационной функции.
        /// </summary>
        private ActivationFuncDelegate _afd;

        #endregion

        #region Методы

        /// <summary>
        /// Сумматор входных сигналов нейрона. Нужен для передачи значения в активационную функцию.
        /// </summary>
        /// <returns>Сумма значений входных сигналов, умноженных на весовые коэффициенты.</returns>
        private double Sum()
        {
            double res = 0;
            for (int i = 0; i < SignalsIn.Length; ++i )
                res += SignalsIn[i] * _weights[i];
            return res;
        }

        /// <summary>
        /// Возвращает значение сигнала активационной функции нейрона.
        /// </summary>
        /// <param name="param">Параметр(коэфициент) функции.</param>
        /// <returns>Сигнал, возвращаемый нейроном.</returns>
        public double SignalsOut(double param)
        {
            //куда дальше передавать сигнал решается на уровне слоя (Layer)
            return _afd(param);
        }

        #endregion
    }
}
