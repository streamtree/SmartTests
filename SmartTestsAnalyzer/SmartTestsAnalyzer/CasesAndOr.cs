﻿using System.Collections.Generic;
using System.Text;

#if !EXTENSION
using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SmartTestsAnalyzer.Criterias;

#endif



namespace SmartTestsAnalyzer
{
    /// <summary>
    ///     A list of test cases (CriteriaAnalysis of Criteria subclasses) combined with logical AND to represents the criteria
    ///     of
    ///     a parameter of a tested member.
    /// </summary>
    /// <remarks>
    ///     It is used for user test cases and for expected test cases (when computing all expected test cases)
    /// </remarks>
    /// <example>
    ///     <para>'Case( Criteria1.Good1 )': Good1 field of Criteria1</para>
    ///     <para>
    ///         'Case( Criteria1.Good1 | Criteria1.Good2 )': Good1 field of Criteria1 is one CriteriaAnd and Good2 field of
    ///         Criteria1 is another one
    ///     </para>
    ///     <para>
    ///         'Case( Criteria1.Good1 & Criteria2.GoodA )': Good1 field of Criteria1 and GoodB field of Criteria2 is one
    ///         CriteriaAnd
    ///     </para>
    ///     <para>
    ///         'Case( Criteria1.Good1 > Criteria2.GoodA )': Good1 field of Criteria1 and GoodB field of Criteria2 is one
    ///         CriteriaAnd
    ///     </para>
    /// </example>
    public class CasesAndOr
    {
#if EXTENSION
        public List<CasesAnd> CasesAnd { get; } = new List<CasesAnd>();

#else
        public CasesAndOr()
        { }


        private CasesAndOr( string parameterName, CriteriaAnalysis criteria, bool hasError )
        {
            CasesAnd.Add( new CasesAnd( null, parameterName, null, criteria, hasError ) );
        }


        public CasesAndOr( ExpressionSyntax casesExpression, ExpressionSyntax parameterNameExpression, string parameterName, CriteriaAnalysis criteria, bool hasError )
        {
            CasesAnd.Add( new CasesAnd( parameterNameExpression, parameterName, casesExpression, criteria, hasError ) );
        }


        public List<CasesAnd> CasesAnd { get; } = new List<CasesAnd>();


        public void CombineAnd( CasesAndOr cases )
        {
            if( CasesAnd.Count == 0 )
            {
                // No criteria yet!
                // => Take it as it
                CasesAnd.AddRange( cases.CasesAnd );
                return;
            }

            var currentCriterias = CasesAnd.ToList();
            CasesAnd.Clear();
            var otherErrors = new HashSet<CasesAnd>();
            foreach( var combinedCriterias in currentCriterias )
            {
                if( combinedCriterias.HasError )
                {
                    // Do not combine it
                    CasesAnd.Add( combinedCriterias );
                    continue;
                }

                // Combine it
                foreach( var otherCriterias in cases.CasesAnd )
                {
                    if( otherCriterias.HasError )
                    {
                        if( otherErrors.Add( otherCriterias ) )
                            // Do not combine it
                            CasesAnd.Add( otherCriterias );
                        continue;
                    }

                    CasesAnd.Add( combinedCriterias.CombineAnd( otherCriterias ) );
                }
            }
        }


        public void CombineOr( CasesAndOr cases ) => CasesAnd.AddRange( cases.CasesAnd );


        public void Validate( TestedMember testedMember, INamedTypeSymbol errorType, Action<Diagnostic> reportError )
        {
            var allCases = ComputeAllCases( errorType );

            foreach( var casesAnd in CasesAnd )
                allCases.CasesAnd.Remove( casesAnd );

            if( allCases.CasesAnd.Count == 0 )
                // No missing cases
                return;

            // Missing cases
            reportError( SmartTestsDiagnostics.CreateMissingCases( testedMember,
                                                                   GetExpressionSyntax(),
                                                                   allCases.ToString() ) );

            foreach( var casesAnd in allCases.CasesAnd )
            {
                casesAnd.IsMissing = true;
                CasesAnd.Add( casesAnd );
            }
        }


        private IList<ExpressionSyntax> GetExpressionSyntax()
        {
            var result = new List<ExpressionSyntax>();
            foreach( var caseAnd in CasesAnd )
                caseAnd.FillExpressionSyntax( result );
            return result;
        }


        private CasesAndOr ComputeAllCases( INamedTypeSymbol errorType )
        {
            var result = new CasesAndOr();
            var values = GetAllValues( errorType );
            foreach( var pair in values )
                result.CombineAnd( ComputeAllCases( pair.Key, pair.Value.Values ) );
            return result;
        }


        private Dictionary<string, Dictionary<string, CriteriaValues>> GetAllValues( INamedTypeSymbol errorType )
        {
            var result = new Dictionary<string, Dictionary<string, CriteriaValues>>();
            foreach( var criteria in CasesAnd )
                criteria.FillCriteriaValues( result, errorType );
            foreach( var resultValue in result.Values )
            foreach( var criteriaValue in resultValue.Values )
                criteriaValue.AddCurrentValues();
            return result;
        }


        private static CasesAndOr ComputeAllCases( string parameterName, Dictionary<string, CriteriaValues>.ValueCollection criteriaValues )
        {
            var result = new CasesAndOr();
            foreach( var criteriaValue in criteriaValues )
            {
                var cases = new CasesAndOr();
                foreach( var criterion in criteriaValue.Values )
                    cases.CombineOr( new CasesAndOr( parameterName, criterion.Analysis, criterion.IsError ) );
                result.CombineAnd( cases );
            }

            return result;
        }
#endif


        public void ToString( StringBuilder result )
        {
            foreach( var criterias in CasesAnd )
            {
                criterias.ToString( result );
                result.Append( " and " );
            }

            result.Length -= 5;
        }


        public override string ToString()
        {
            var result = new StringBuilder();
            ToString( result );
            return result.ToString();
        }
    }
}