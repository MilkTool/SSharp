/*
 * Copyright � 2011, Petro Protsyk, Denys Vuika
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 *  
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using Scripting.SSharp.Parser.Ast;

namespace Scripting.SSharp.Parser
{
  internal interface IGrammarTerm
  {
    string Name { get; }
    string DisplayName { get; }
    Type NodeType { get; }

    bool IsSet(TermOptions termOptions);
  }

  internal interface ITerminal : IGrammarTerm
  {
    string Key { get; }
    TokenCategory Category { get; }
    TokenMatchMode MatchMode { get; }
    int Precedence { get; }
    Associativity Associativity { get; }
    int Priority { get; }
    TokenAst TryMatch(CompilerContext context, ISourceStream source);    
  }

  public class AstNodeList : List<AstNode> { }

  internal class StringDictionary : Dictionary<string, string> { }
  internal class CharList : List<char> { }
  internal class TokenList : List<TokenAst> { }
  internal class TerminalList : List<ITerminal> { }
  internal class BnfExpressionData : List<BnfTermList> { }
  internal class BnfTermList : List<GrammarTerm> { }
  internal class NonTerminalList : List<NonTerminal> { }
  internal class SyntaxErrorList : List<SyntaxError> { }
  internal class EscapeTable : Dictionary<char, char> { }
  internal class LR0ItemList : List<LR0Item> { }
  internal class ProductionList : List<Production> { }
  internal class LRItemList : List<LRItem> { }
  internal class ActionRecordTable : Dictionary<string, ActionRecord> { }
  internal class ParserStateList : List<ParserState> { }
  internal class ParserStateTable : Dictionary<string, ParserState> { }
  internal class TerminalLookupTable : Dictionary<char, TerminalList> { }
  internal class UnicodeCategoryList : List<UnicodeCategory> { }

#if !SILVERLIGHT
  internal class StringSet : HashSet<string>
  {
    public void AddRange(IEnumerable<string> values)
    {
      foreach (string value in values) Add(value);
    }
  }
#else
  internal class StringSet : ICollection<string>
  {
    Dictionary<string, bool> stringValues = new Dictionary<string, bool>();

    public void AddRange(IEnumerable<string> values)
    {
      foreach (string value in values)
        if (!stringValues.ContainsKey(value))
          Add(value);
    }
    public override string ToString()
    {
      return ToString(" ");
    }
    public string ToString(string separator)
    {
      return String.Join(separator, stringValues.Keys.ToArray());
    }

  #region ICollection<string> Members

    public void Add(string item)
    {
      if (!stringValues.ContainsKey(item))
        stringValues.Add(item, true);
    }

    public void Clear()
    {
      stringValues.Clear();
    }

    public bool Contains(string item)
    {
      return stringValues.ContainsKey(item);
    }

    public void CopyTo(string[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get { return stringValues.Count; }
    }

    public bool IsReadOnly
    {
      get { return false; }
    }

    public bool Remove(string item)
    {
      if (!stringValues.ContainsKey(item))
      {
        stringValues.Remove(item);
        return true;
      }

      return false;
    }

  #endregion

  #region IEnumerable<string> Members

    public IEnumerator<string> GetEnumerator()
    {
      return stringValues.Keys.GetEnumerator();
    }

  #endregion

  #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()
    {
      return stringValues.Keys.GetEnumerator();
    }

  #endregion
  }
#endif
  internal class StringList : List<string>
  {
    public StringList() { }
    
    public StringList(params string[] args)
    {
      AddRange(args);
    }

    public new void AddRange(IEnumerable<string> keys)
    {
      foreach (string key in keys)
        this.Add(key);
    }
   
    public override string ToString()
    {
      return ToString(" ");
    }
    
    public string ToString(string separator)
    {
      return String.Join(separator, ToArray());
    }

    public static int LongerFirst(string x, string y)
    {
      try
      {
        if (x.Length > y.Length) return -1;
      }
      catch { }
      return 0;
    }

  }

}
