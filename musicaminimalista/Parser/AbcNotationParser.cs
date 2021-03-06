//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from AbcNotation.g4 by ANTLR 4.5.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591


	using MusicaMinimalista.Objects.Music;
	using MusicaMinimalista.Objects.Utils;

using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.1")]
[System.CLSCompliant(false)]
public partial class AbcNotationParser : Parser {
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, XField=9, 
		TField=10, VField=11, KField=12, MField=13, QField=14, LFieldHeader=15, 
		Num=16, PitchSymbol=17, Accidental=18, Lbrack=19, Rbrack=20, NumSymbol=21, 
		WhiteSpace=22, Remark=23, NewLine=24, LineComment=25;
	public const int
		RULE_file = 0, RULE_header = 1, RULE_x_field = 2, RULE_t_field = 3, RULE_m_field = 4, 
		RULE_q_field = 5, RULE_l_field = 6, RULE_k_field = 7, RULE_score = 8, 
		RULE_v_field = 9, RULE_music_elem = 10, RULE_ignored_elem = 11, RULE_chord = 12, 
		RULE_note = 13, RULE_noteduration = 14, RULE_pitch = 15, RULE_bar = 16;
	public static readonly string[] ruleNames = {
		"file", "header", "x_field", "t_field", "m_field", "q_field", "l_field", 
		"k_field", "score", "v_field", "music_elem", "ignored_elem", "chord", 
		"note", "noteduration", "pitch", "bar"
	};

	private static readonly string[] _LiteralNames = {
		null, "'/'", "'|'", "'|]'", "'||'", "'[|'", "'|:'", "':|'", "'::'", null, 
		null, null, null, null, null, "'L:'", null, null, null, "'['", "']'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, "XField", "TField", 
		"VField", "KField", "MField", "QField", "LFieldHeader", "Num", "PitchSymbol", 
		"Accidental", "Lbrack", "Rbrack", "NumSymbol", "WhiteSpace", "Remark", 
		"NewLine", "LineComment"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "AbcNotation.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }


		AbcNoteParser noteParser = new AbcNoteParser();
		List<string> voiceNames = new List<string>();
		int currentVoice = 0;

		public void changeToVoice(string voiceStr, Motif motif){
			if (voiceNames.Count == 0)
			{
				voiceNames.Add(voiceStr);
				return;
			}

			for (int i = 0; i < voiceNames.Count; i++)
			{
				if (voiceNames[i] == voiceStr) currentVoice = i;
			}

			voiceNames.Add(voiceStr);
			motif.addVoice(new Voice());
			currentVoice = voiceNames.Count - 1;
		}

	public AbcNotationParser(ITokenStream input)
		: base(input)
	{
		Interpreter = new ParserATNSimulator(this,_ATN);
	}
	public partial class FileContext : ParserRuleContext {
		public Motif m;
		public HeaderContext h;
		public ScoreContext s;
		public HeaderContext header() {
			return GetRuleContext<HeaderContext>(0);
		}
		public ScoreContext score() {
			return GetRuleContext<ScoreContext>(0);
		}
		public FileContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_file; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterFile(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitFile(this);
		}
	}

	[RuleVersion(0)]
	public FileContext file() {
		FileContext _localctx = new FileContext(Context, State);
		EnterRule(_localctx, 0, RULE_file);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 34; _localctx.h = header();
			}
			{
			State = 35; _localctx.s = score();
			 _localctx.m =  _localctx.s.m; _localctx.m.setTonality(_localctx.h.tonality);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class HeaderContext : ParserRuleContext {
		public int referenceNumber;
		public string title;
		public Tonality tonality;
		public string length;
		public X_fieldContext x;
		public T_fieldContext t;
		public M_fieldContext m;
		public L_fieldContext l;
		public Q_fieldContext q;
		public K_fieldContext k;
		public X_fieldContext x_field() {
			return GetRuleContext<X_fieldContext>(0);
		}
		public T_fieldContext t_field() {
			return GetRuleContext<T_fieldContext>(0);
		}
		public K_fieldContext k_field() {
			return GetRuleContext<K_fieldContext>(0);
		}
		public M_fieldContext[] m_field() {
			return GetRuleContexts<M_fieldContext>();
		}
		public M_fieldContext m_field(int i) {
			return GetRuleContext<M_fieldContext>(i);
		}
		public L_fieldContext[] l_field() {
			return GetRuleContexts<L_fieldContext>();
		}
		public L_fieldContext l_field(int i) {
			return GetRuleContext<L_fieldContext>(i);
		}
		public Q_fieldContext[] q_field() {
			return GetRuleContexts<Q_fieldContext>();
		}
		public Q_fieldContext q_field(int i) {
			return GetRuleContext<Q_fieldContext>(i);
		}
		public HeaderContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_header; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterHeader(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitHeader(this);
		}
	}

	[RuleVersion(0)]
	public HeaderContext header() {
		HeaderContext _localctx = new HeaderContext(Context, State);
		EnterRule(_localctx, 2, RULE_header);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 38; _localctx.x = x_field();
			_localctx.referenceNumber =  _localctx.x.referenceNumber;
			}
			{
			State = 41; _localctx.t = t_field();
			_localctx.title =  _localctx.t.title;
			}
			State = 49;
			ErrorHandler.Sync(this);
			_la = TokenStream.La(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << MField) | (1L << QField) | (1L << LFieldHeader))) != 0)) {
				{
				State = 47;
				switch (TokenStream.La(1)) {
				case MField:
					{
					State = 44; _localctx.m = m_field();
					}
					break;
				case LFieldHeader:
					{
					State = 45; _localctx.l = l_field();
					}
					break;
				case QField:
					{
					State = 46; _localctx.q = q_field();
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				State = 51;
				ErrorHandler.Sync(this);
				_la = TokenStream.La(1);
			}
			{
			State = 52; _localctx.k = k_field();

						_localctx.tonality =  Tonality.parse(_localctx.k.tonality_str);
						noteParser.setTonality(_localctx.tonality);
					
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class X_fieldContext : ParserRuleContext {
		public int referenceNumber;
		public IToken x;
		public ITerminalNode XField() { return GetToken(AbcNotationParser.XField, 0); }
		public X_fieldContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_x_field; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterX_field(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitX_field(this);
		}
	}

	[RuleVersion(0)]
	public X_fieldContext x_field() {
		X_fieldContext _localctx = new X_fieldContext(Context, State);
		EnterRule(_localctx, 4, RULE_x_field);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 55; _localctx.x = Match(XField);
			 _localctx.referenceNumber =  int.Parse(((_localctx.x!=null?_localctx.x.Text:null)).Substring(2)); 
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class T_fieldContext : ParserRuleContext {
		public string title;
		public IToken t;
		public ITerminalNode TField() { return GetToken(AbcNotationParser.TField, 0); }
		public T_fieldContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_t_field; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterT_field(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitT_field(this);
		}
	}

	[RuleVersion(0)]
	public T_fieldContext t_field() {
		T_fieldContext _localctx = new T_fieldContext(Context, State);
		EnterRule(_localctx, 6, RULE_t_field);
		try {
			State = 61;
			switch (TokenStream.La(1)) {
			case TField:
				EnterOuterAlt(_localctx, 1);
				{
				{
				State = 58; _localctx.t = Match(TField);
				 _localctx.title =  ((_localctx.t!=null?_localctx.t.Text:null)).Substring(2); 
				}
				}
				break;
			case Eof:
			case T__1:
			case T__2:
			case T__3:
			case T__4:
			case T__5:
			case T__6:
			case T__7:
			case VField:
			case KField:
			case MField:
			case QField:
			case LFieldHeader:
			case PitchSymbol:
			case Accidental:
			case Lbrack:
			case WhiteSpace:
			case Remark:
				EnterOuterAlt(_localctx, 2);
				{
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class M_fieldContext : ParserRuleContext {
		public IToken m;
		public ITerminalNode MField() { return GetToken(AbcNotationParser.MField, 0); }
		public M_fieldContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_m_field; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterM_field(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitM_field(this);
		}
	}

	[RuleVersion(0)]
	public M_fieldContext m_field() {
		M_fieldContext _localctx = new M_fieldContext(Context, State);
		EnterRule(_localctx, 8, RULE_m_field);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 63; _localctx.m = Match(MField);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Q_fieldContext : ParserRuleContext {
		public IToken q;
		public ITerminalNode QField() { return GetToken(AbcNotationParser.QField, 0); }
		public Q_fieldContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_q_field; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterQ_field(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitQ_field(this);
		}
	}

	[RuleVersion(0)]
	public Q_fieldContext q_field() {
		Q_fieldContext _localctx = new Q_fieldContext(Context, State);
		EnterRule(_localctx, 10, RULE_q_field);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 65; _localctx.q = Match(QField);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class L_fieldContext : ParserRuleContext {
		public NotedurationContext d;
		public ITerminalNode LFieldHeader() { return GetToken(AbcNotationParser.LFieldHeader, 0); }
		public NotedurationContext noteduration() {
			return GetRuleContext<NotedurationContext>(0);
		}
		public L_fieldContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_l_field; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterL_field(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitL_field(this);
		}
	}

	[RuleVersion(0)]
	public L_fieldContext l_field() {
		L_fieldContext _localctx = new L_fieldContext(Context, State);
		EnterRule(_localctx, 12, RULE_l_field);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 67; Match(LFieldHeader);
			State = 68; _localctx.d = noteduration();
			noteParser.setUnitNoteLength(_localctx.d.nd);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class K_fieldContext : ParserRuleContext {
		public string tonality_str;
		public IToken k;
		public ITerminalNode KField() { return GetToken(AbcNotationParser.KField, 0); }
		public K_fieldContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_k_field; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterK_field(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitK_field(this);
		}
	}

	[RuleVersion(0)]
	public K_fieldContext k_field() {
		K_fieldContext _localctx = new K_fieldContext(Context, State);
		EnterRule(_localctx, 14, RULE_k_field);
		try {
			State = 74;
			switch (TokenStream.La(1)) {
			case KField:
				EnterOuterAlt(_localctx, 1);
				{
				{
				State = 71; _localctx.k = Match(KField);
				 _localctx.tonality_str =  ((_localctx.k!=null?_localctx.k.Text:null)).Substring(2); 
				}
				}
				break;
			case Eof:
			case T__1:
			case T__2:
			case T__3:
			case T__4:
			case T__5:
			case T__6:
			case T__7:
			case VField:
			case PitchSymbol:
			case Accidental:
			case Lbrack:
			case WhiteSpace:
			case Remark:
				EnterOuterAlt(_localctx, 2);
				{
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ScoreContext : ParserRuleContext {
		public Motif m;
		public Music_elemContext me;
		public V_fieldContext v;
		public BarContext[] bar() {
			return GetRuleContexts<BarContext>();
		}
		public BarContext bar(int i) {
			return GetRuleContext<BarContext>(i);
		}
		public Ignored_elemContext[] ignored_elem() {
			return GetRuleContexts<Ignored_elemContext>();
		}
		public Ignored_elemContext ignored_elem(int i) {
			return GetRuleContext<Ignored_elemContext>(i);
		}
		public Music_elemContext[] music_elem() {
			return GetRuleContexts<Music_elemContext>();
		}
		public Music_elemContext music_elem(int i) {
			return GetRuleContext<Music_elemContext>(i);
		}
		public V_fieldContext[] v_field() {
			return GetRuleContexts<V_fieldContext>();
		}
		public V_fieldContext v_field(int i) {
			return GetRuleContext<V_fieldContext>(i);
		}
		public ScoreContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_score; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterScore(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitScore(this);
		}
	}

	[RuleVersion(0)]
	public ScoreContext score() {
		ScoreContext _localctx = new ScoreContext(Context, State);
		EnterRule(_localctx, 16, RULE_score);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			 _localctx.m =  new Motif(); _localctx.m.addVoice(new Voice());
			State = 89;
			ErrorHandler.Sync(this);
			_la = TokenStream.La(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << VField) | (1L << PitchSymbol) | (1L << Accidental) | (1L << Lbrack) | (1L << WhiteSpace) | (1L << Remark))) != 0)) {
				{
				State = 87;
				switch (TokenStream.La(1)) {
				case PitchSymbol:
				case Accidental:
				case Lbrack:
					{
					{
					State = 77; _localctx.me = music_elem();
					_localctx.m.getVoice(currentVoice).add(_localctx.me.mi);
					}
					}
					break;
				case T__1:
				case T__2:
				case T__3:
				case T__4:
				case T__5:
				case T__6:
				case T__7:
					{
					{
					State = 80; bar();
					noteParser.resetAccidentals();
					}
					}
					break;
				case WhiteSpace:
				case Remark:
					{
					{
					State = 83; ignored_elem();
					}
					}
					break;
				case VField:
					{
					{
					State = 84; _localctx.v = v_field();

										changeToVoice(_localctx.v.voice_str, _localctx.m);
									
					}
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				State = 91;
				ErrorHandler.Sync(this);
				_la = TokenStream.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class V_fieldContext : ParserRuleContext {
		public string voice_str;
		public IToken v;
		public ITerminalNode VField() { return GetToken(AbcNotationParser.VField, 0); }
		public V_fieldContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_v_field; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterV_field(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitV_field(this);
		}
	}

	[RuleVersion(0)]
	public V_fieldContext v_field() {
		V_fieldContext _localctx = new V_fieldContext(Context, State);
		EnterRule(_localctx, 18, RULE_v_field);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 92; _localctx.v = Match(VField);
			 _localctx.voice_str =  ((_localctx.v!=null?_localctx.v.Text:null)).Substring(2); 
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Music_elemContext : ParserRuleContext {
		public MusicItem mi;
		public NoteContext n;
		public ChordContext c;
		public NoteContext note() {
			return GetRuleContext<NoteContext>(0);
		}
		public ChordContext chord() {
			return GetRuleContext<ChordContext>(0);
		}
		public Music_elemContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_music_elem; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterMusic_elem(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitMusic_elem(this);
		}
	}

	[RuleVersion(0)]
	public Music_elemContext music_elem() {
		Music_elemContext _localctx = new Music_elemContext(Context, State);
		EnterRule(_localctx, 20, RULE_music_elem);
		try {
			State = 101;
			switch (TokenStream.La(1)) {
			case PitchSymbol:
			case Accidental:
				EnterOuterAlt(_localctx, 1);
				{
				{
				State = 95; _localctx.n = note();
				_localctx.mi =  _localctx.n.n;
				}
				}
				break;
			case Lbrack:
				EnterOuterAlt(_localctx, 2);
				{
				{
				State = 98; _localctx.c = chord();
				_localctx.mi =  _localctx.c.c;
				}
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Ignored_elemContext : ParserRuleContext {
		public ITerminalNode WhiteSpace() { return GetToken(AbcNotationParser.WhiteSpace, 0); }
		public ITerminalNode Remark() { return GetToken(AbcNotationParser.Remark, 0); }
		public Ignored_elemContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_ignored_elem; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterIgnored_elem(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitIgnored_elem(this);
		}
	}

	[RuleVersion(0)]
	public Ignored_elemContext ignored_elem() {
		Ignored_elemContext _localctx = new Ignored_elemContext(Context, State);
		EnterRule(_localctx, 22, RULE_ignored_elem);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 103;
			_la = TokenStream.La(1);
			if ( !(_la==WhiteSpace || _la==Remark) ) {
			ErrorHandler.RecoverInline(this);
			}
			else {
			    Consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ChordContext : ParserRuleContext {
		public Chord c;
		public NoteContext n;
		public ITerminalNode Lbrack() { return GetToken(AbcNotationParser.Lbrack, 0); }
		public ITerminalNode Rbrack() { return GetToken(AbcNotationParser.Rbrack, 0); }
		public NoteContext[] note() {
			return GetRuleContexts<NoteContext>();
		}
		public NoteContext note(int i) {
			return GetRuleContext<NoteContext>(i);
		}
		public ChordContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_chord; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterChord(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitChord(this);
		}
	}

	[RuleVersion(0)]
	public ChordContext chord() {
		ChordContext _localctx = new ChordContext(Context, State);
		EnterRule(_localctx, 24, RULE_chord);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			_localctx.c =  new Chord();
			State = 106; Match(Lbrack);
			State = 110;
			ErrorHandler.Sync(this);
			_la = TokenStream.La(1);
			do {
				{
				{
				State = 107; _localctx.n = note();
				_localctx.c.add(_localctx.n.n);
				}
				}
				State = 112;
				ErrorHandler.Sync(this);
				_la = TokenStream.La(1);
			} while ( _la==PitchSymbol || _la==Accidental );
			State = 114; Match(Rbrack);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class NoteContext : ParserRuleContext {
		public Note n;
		public PitchContext p;
		public NotedurationContext nd;
		public PitchContext pitch() {
			return GetRuleContext<PitchContext>(0);
		}
		public NotedurationContext noteduration() {
			return GetRuleContext<NotedurationContext>(0);
		}
		public NoteContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_note; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterNote(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitNote(this);
		}
	}

	[RuleVersion(0)]
	public NoteContext note() {
		NoteContext _localctx = new NoteContext(Context, State);
		EnterRule(_localctx, 26, RULE_note);
		try {
			State = 123;
			switch ( Interpreter.AdaptivePredict(TokenStream,8,Context) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				{
				State = 116; _localctx.p = pitch();
				State = 117; _localctx.nd = noteduration();
				_localctx.n =  noteParser.parse((_localctx.p!=null?TokenStream.GetText(_localctx.p.Start,_localctx.p.Stop):null), _localctx.nd.nd);
				}
				}
				break;
			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				{
				State = 120; _localctx.p = pitch();
				_localctx.n =  noteParser.parse((_localctx.p!=null?TokenStream.GetText(_localctx.p.Start,_localctx.p.Stop):null), 1);
				}
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class NotedurationContext : ParserRuleContext {
		public Duration nd;
		public IToken a;
		public IToken b;
		public ITerminalNode[] Num() { return GetTokens(AbcNotationParser.Num); }
		public ITerminalNode Num(int i) {
			return GetToken(AbcNotationParser.Num, i);
		}
		public NotedurationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_noteduration; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterNoteduration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitNoteduration(this);
		}
	}

	[RuleVersion(0)]
	public NotedurationContext noteduration() {
		NotedurationContext _localctx = new NotedurationContext(Context, State);
		EnterRule(_localctx, 28, RULE_noteduration);
		try {
			State = 134;
			switch ( Interpreter.AdaptivePredict(TokenStream,9,Context) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				{
				State = 125; _localctx.a = Match(Num);
				_localctx.nd =  int.Parse((_localctx.a!=null?_localctx.a.Text:null));
				}
				}
				break;
			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				{
				State = 127; _localctx.a = Match(Num);
				State = 128; Match(T__0);
				State = 129; _localctx.b = Match(Num);
				_localctx.nd =  new Duration(int.Parse((_localctx.a!=null?_localctx.a.Text:null)), int.Parse((_localctx.b!=null?_localctx.b.Text:null)));
				}
				}
				break;
			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				{
				State = 131; Match(T__0);
				State = 132; _localctx.b = Match(Num);
				_localctx.nd =  new Duration(1, int.Parse((_localctx.b!=null?_localctx.b.Text:null)));
				}
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class PitchContext : ParserRuleContext {
		public IToken a;
		public IToken p;
		public ITerminalNode Accidental() { return GetToken(AbcNotationParser.Accidental, 0); }
		public ITerminalNode PitchSymbol() { return GetToken(AbcNotationParser.PitchSymbol, 0); }
		public PitchContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_pitch; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterPitch(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitPitch(this);
		}
	}

	[RuleVersion(0)]
	public PitchContext pitch() {
		PitchContext _localctx = new PitchContext(Context, State);
		EnterRule(_localctx, 30, RULE_pitch);
		try {
			State = 139;
			switch (TokenStream.La(1)) {
			case Accidental:
				EnterOuterAlt(_localctx, 1);
				{
				State = 136; _localctx.a = Match(Accidental);
				State = 137; _localctx.p = Match(PitchSymbol);
				}
				break;
			case PitchSymbol:
				EnterOuterAlt(_localctx, 2);
				{
				State = 138; _localctx.p = Match(PitchSymbol);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class BarContext : ParserRuleContext {
		public BarContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_bar; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.EnterBar(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAbcNotationListener typedListener = listener as IAbcNotationListener;
			if (typedListener != null) typedListener.ExitBar(this);
		}
	}

	[RuleVersion(0)]
	public BarContext bar() {
		BarContext _localctx = new BarContext(Context, State);
		EnterRule(_localctx, 32, RULE_bar);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 141;
			_la = TokenStream.La(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << T__6) | (1L << T__7))) != 0)) ) {
			ErrorHandler.RecoverInline(this);
			}
			else {
			    Consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public static readonly string _serializedATN =
		"\x3\x430\xD6D1\x8206\xAD2D\x4417\xAEF1\x8D80\xAADD\x3\x1B\x92\x4\x2\t"+
		"\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4\t"+
		"\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x3\x2\x3\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\a\x3\x32\n\x3\f\x3\xE\x3\x35\v"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x5\x5@\n\x5"+
		"\x3\x6\x3\x6\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x5\tM\n\t\x3"+
		"\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\a\nZ\n\n\f\n\xE\n"+
		"]\v\n\x3\v\x3\v\x3\v\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x5\fh\n\f\x3\r\x3\r"+
		"\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x6\xEq\n\xE\r\xE\xE\xEr\x3\xE\x3\xE\x3"+
		"\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x5\xF~\n\xF\x3\x10\x3\x10\x3\x10"+
		"\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x5\x10\x89\n\x10\x3\x11\x3"+
		"\x11\x3\x11\x5\x11\x8E\n\x11\x3\x12\x3\x12\x3\x12\x2\x2\x13\x2\x4\x6\b"+
		"\n\f\xE\x10\x12\x14\x16\x18\x1A\x1C\x1E \"\x2\x4\x3\x2\x18\x19\x3\x2\x4"+
		"\n\x8F\x2$\x3\x2\x2\x2\x4(\x3\x2\x2\x2\x6\x39\x3\x2\x2\x2\b?\x3\x2\x2"+
		"\x2\n\x41\x3\x2\x2\x2\f\x43\x3\x2\x2\x2\xE\x45\x3\x2\x2\x2\x10L\x3\x2"+
		"\x2\x2\x12N\x3\x2\x2\x2\x14^\x3\x2\x2\x2\x16g\x3\x2\x2\x2\x18i\x3\x2\x2"+
		"\x2\x1Ak\x3\x2\x2\x2\x1C}\x3\x2\x2\x2\x1E\x88\x3\x2\x2\x2 \x8D\x3\x2\x2"+
		"\x2\"\x8F\x3\x2\x2\x2$%\x5\x4\x3\x2%&\x5\x12\n\x2&\'\b\x2\x1\x2\'\x3\x3"+
		"\x2\x2\x2()\x5\x6\x4\x2)*\b\x3\x1\x2*+\x3\x2\x2\x2+,\x5\b\x5\x2,-\b\x3"+
		"\x1\x2-\x33\x3\x2\x2\x2.\x32\x5\n\x6\x2/\x32\x5\xE\b\x2\x30\x32\x5\f\a"+
		"\x2\x31.\x3\x2\x2\x2\x31/\x3\x2\x2\x2\x31\x30\x3\x2\x2\x2\x32\x35\x3\x2"+
		"\x2\x2\x33\x31\x3\x2\x2\x2\x33\x34\x3\x2\x2\x2\x34\x36\x3\x2\x2\x2\x35"+
		"\x33\x3\x2\x2\x2\x36\x37\x5\x10\t\x2\x37\x38\b\x3\x1\x2\x38\x5\x3\x2\x2"+
		"\x2\x39:\a\v\x2\x2:;\b\x4\x1\x2;\a\x3\x2\x2\x2<=\a\f\x2\x2=@\b\x5\x1\x2"+
		">@\x3\x2\x2\x2?<\x3\x2\x2\x2?>\x3\x2\x2\x2@\t\x3\x2\x2\x2\x41\x42\a\xF"+
		"\x2\x2\x42\v\x3\x2\x2\x2\x43\x44\a\x10\x2\x2\x44\r\x3\x2\x2\x2\x45\x46"+
		"\a\x11\x2\x2\x46G\x5\x1E\x10\x2GH\b\b\x1\x2H\xF\x3\x2\x2\x2IJ\a\xE\x2"+
		"\x2JM\b\t\x1\x2KM\x3\x2\x2\x2LI\x3\x2\x2\x2LK\x3\x2\x2\x2M\x11\x3\x2\x2"+
		"\x2N[\b\n\x1\x2OP\x5\x16\f\x2PQ\b\n\x1\x2QZ\x3\x2\x2\x2RS\x5\"\x12\x2"+
		"ST\b\n\x1\x2TZ\x3\x2\x2\x2UZ\x5\x18\r\x2VW\x5\x14\v\x2WX\b\n\x1\x2XZ\x3"+
		"\x2\x2\x2YO\x3\x2\x2\x2YR\x3\x2\x2\x2YU\x3\x2\x2\x2YV\x3\x2\x2\x2Z]\x3"+
		"\x2\x2\x2[Y\x3\x2\x2\x2[\\\x3\x2\x2\x2\\\x13\x3\x2\x2\x2][\x3\x2\x2\x2"+
		"^_\a\r\x2\x2_`\b\v\x1\x2`\x15\x3\x2\x2\x2\x61\x62\x5\x1C\xF\x2\x62\x63"+
		"\b\f\x1\x2\x63h\x3\x2\x2\x2\x64\x65\x5\x1A\xE\x2\x65\x66\b\f\x1\x2\x66"+
		"h\x3\x2\x2\x2g\x61\x3\x2\x2\x2g\x64\x3\x2\x2\x2h\x17\x3\x2\x2\x2ij\t\x2"+
		"\x2\x2j\x19\x3\x2\x2\x2kl\b\xE\x1\x2lp\a\x15\x2\x2mn\x5\x1C\xF\x2no\b"+
		"\xE\x1\x2oq\x3\x2\x2\x2pm\x3\x2\x2\x2qr\x3\x2\x2\x2rp\x3\x2\x2\x2rs\x3"+
		"\x2\x2\x2st\x3\x2\x2\x2tu\a\x16\x2\x2u\x1B\x3\x2\x2\x2vw\x5 \x11\x2wx"+
		"\x5\x1E\x10\x2xy\b\xF\x1\x2y~\x3\x2\x2\x2z{\x5 \x11\x2{|\b\xF\x1\x2|~"+
		"\x3\x2\x2\x2}v\x3\x2\x2\x2}z\x3\x2\x2\x2~\x1D\x3\x2\x2\x2\x7F\x80\a\x12"+
		"\x2\x2\x80\x89\b\x10\x1\x2\x81\x82\a\x12\x2\x2\x82\x83\a\x3\x2\x2\x83"+
		"\x84\a\x12\x2\x2\x84\x89\b\x10\x1\x2\x85\x86\a\x3\x2\x2\x86\x87\a\x12"+
		"\x2\x2\x87\x89\b\x10\x1\x2\x88\x7F\x3\x2\x2\x2\x88\x81\x3\x2\x2\x2\x88"+
		"\x85\x3\x2\x2\x2\x89\x1F\x3\x2\x2\x2\x8A\x8B\a\x14\x2\x2\x8B\x8E\a\x13"+
		"\x2\x2\x8C\x8E\a\x13\x2\x2\x8D\x8A\x3\x2\x2\x2\x8D\x8C\x3\x2\x2\x2\x8E"+
		"!\x3\x2\x2\x2\x8F\x90\t\x3\x2\x2\x90#\x3\x2\x2\x2\r\x31\x33?LY[gr}\x88"+
		"\x8D";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
