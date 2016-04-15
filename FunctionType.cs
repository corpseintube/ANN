namespace Neuronets
{
    /// <summary>
    /// Перечисление видов активационной функции формального нейрона.
    /// </summary>
    enum FunctionType
    {
        /// <summary>
        /// Линейная функция. Диапазон значений [-∞; +∞]
        /// </summary>
        Linear,
        /// <summary>
        /// Сигмоидальная функция. Диапазон значений [0; 1].
        /// </summary>
        Sigmoid,
        /// <summary>
        /// Пороговая функция. Принимает значения 0 или 1.
        /// </summary>
        Threshold,
        /// <summary>
        /// Гиперболическая тангенсальная функция. Диапазон значений [-1; 1]
        /// </summary>
        HyperbolicTangent
    }
}
