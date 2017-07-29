﻿using JetBrains.Annotations;



namespace SmartTests.Criterias
{
    /// <summary>
    ///     Provides criterions for a value that should be between a lower bound (inclusively) and an upper bound (exclusively)
    /// </summary>
    [PublicAPI]
    public class MinIncMaxExc: Criteria
    {
        /// <summary>
        ///     When the value is below the lower bound
        ///     <para>THIS IS AN ERROR</para>
        /// </summary>
        [Error]
        public static readonly Criteria IsBelowMin = new MinIncMaxInc();
        /// <summary>
        ///     When the value is equal to the lower bound
        /// </summary>
        public static readonly Criteria IsMin = new MinIncMaxInc();
        /// <summary>
        ///     When the value is beetwween the lower bound and upper bound exclusively
        /// </summary>
        public static readonly Criteria IsBetweenMinMax = new MinIncMaxInc();
        /// <summary>
        ///     When the value is equal to the upper bound
        ///     <para>THIS IS AN ERROR</para>
        /// </summary>
        [Error]
        public static readonly Criteria IsMax = new MinIncMaxInc();
        /// <summary>
        ///     When the value is above to the upper bound
        ///     <para>THIS IS AN ERROR</para>
        /// </summary>
        [Error]
        public static readonly Criteria IsAboveMax = new MinIncMaxInc();
    }
}