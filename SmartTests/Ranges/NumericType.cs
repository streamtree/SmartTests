﻿using System;
using System.Collections.Generic;
using System.Linq;



namespace SmartTests.Ranges
{
    /// <summary>
    ///     Represents a Range of numeric values (with several chunks)
    /// </summary>
    public abstract class NumericType<T, TType>: IType<T>
        where T: IComparable<T>
        where TType: class, IType<T>
    {
        #region MinValue Property

        T IType<T>.MinValue => MinValue;

        /// <inheritdoc cref="MinValue" />
        protected abstract T MinValue { get; }

        #endregion


        #region MaxValue Property

        T IType<T>.MaxValue => MaxValue;

        /// <inheritdoc cref="MaxValue" />
        protected abstract T MaxValue { get; }

        #endregion


        #region GetPrevious Method

        T IType<T>.GetPrevious( T n ) => GetPrevious( n );


        /// <inheritdoc cref="GetPrevious(T)" />
        protected abstract T GetPrevious( T n );

        #endregion


        #region GetNext Method

        T IType<T>.GetNext( T n ) => GetNext( n );


        /// <inheritdoc cref="GetNext(T)" />
        protected abstract T GetNext( T n );

        #endregion


        /// <inheritdoc />
        public List<Chunk<T>> Chunks { get; } = new List<Chunk<T>>();


        /// <inheritdoc />
        public IType<T> Range( T min, T max )
        {
            if( min.CompareTo( max ) > 0 )
                throw new ArgumentException( "min should be lower or equal to max" );

            if( Chunks.Count == 0 )
            {
                Chunks.Add( new Chunk<T>( min, max ) );
                return this;
            }

            var lastChunk = Chunks.Last();
            if( min.CompareTo( lastChunk.Max ) > 0 )
            {
                if( GetPrevious( min ).CompareTo( lastChunk.Max ) == 0 )
                {
                    // Why not having the right Chunk at first?
                    Chunks.RemoveAt( Chunks.Count - 1 );
                    min = lastChunk.Min;
                }

                Chunks.Add( new Chunk<T>( min, max ) );
                return this;
            }

            // Not in the right order!
            GetLocation( min, out var minInChunk, out var minIndex );
            GetLocation( max, out var maxInChunk, out var maxIndex );

            if( minInChunk )
                // In a chunk
                min = Chunks[ minIndex ].Min;
            if( maxInChunk )
                // In a chunk
                max = Chunks[ maxIndex ].Max;

            if( minInChunk )
            {
                if( maxInChunk )
                    Chunks.RemoveRange( minIndex, maxIndex - minIndex + 1 );
                else
                    Chunks.RemoveRange( minIndex, maxIndex - minIndex );
            }
            else
            {
                if( maxInChunk )
                    Chunks.RemoveRange( minIndex, maxIndex - minIndex + 1 );
                else
                    Chunks.RemoveRange( minIndex, maxIndex - minIndex );
            }

            Chunks.Insert( minIndex, new Chunk<T>( min, max ) );
            return this;
        }


        private void GetLocation( T value, out bool inChunk, out int chunkIndex )
        {
            for( chunkIndex = 0; chunkIndex < Chunks.Count; chunkIndex++ )
            {
                if( value.CompareTo( Chunks[ chunkIndex ].Min ) < 0 )
                {
                    inChunk = value.CompareTo( GetPrevious( Chunks[ chunkIndex ].Min ) ) == 0;
                    return;
                }

                if( Chunks[ chunkIndex ].Min.CompareTo( value ) <= 0 &&
                    value.CompareTo( Chunks[ chunkIndex ].Max.CompareTo( MaxValue ) == 0 ? MaxValue : GetNext( Chunks[ chunkIndex ].Max ) ) <= 0 )
                {
                    inChunk = true;
                    return;
                }
            }

            inChunk = false;
        }


        /// <inheritdoc />
        public abstract Criteria GetValue( out T value );


        /// <inheritdoc />
        public override bool Equals( object obj ) => Equals( obj as TType );


        /// <summary>
        ///     Compare two IntRange
        /// </summary>
        /// <param name="other">The other IntRange to compare with.</param>
        /// <returns>
        ///     <c>true</c> if <c>this</c> and <paramref name="other" /> have the same <see cref="Chunks" />; <c>false</c>
        ///     otherwise
        /// </returns>
        protected bool Equals( TType other ) => other?.GetType() == typeof(TType) && Equals( Chunks, other.Chunks );


        /// <inheritdoc />
        public override int GetHashCode() => Chunks?.GetHashCode() ?? 0;
    }
}