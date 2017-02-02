/*
cd C:\Users\David\Dropbox\Universidad\TFG\TFG_MusicaMinimalista\TFG_MusicaMinimalista\musicaminimalista
java -jar antlr-4.5.1-complete.jar -Dlanguage=CSharp AbcNotation.g4
*/

/*
	Possible improvements:
	inline K, M, L
	Broken rythm
*/

grammar AbcNotation;

@header {
	using MusicaMinimalista.Objects.Music;
	using MusicaMinimalista.Objects.Utils;
}

@parser::members {
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
}

file returns [Motif m]
	:	(h=header)
		(s=score 	{ $m = $s.m; $m.setTonality($h.tonality);})
	;

header returns [int referenceNumber, string title, Tonality tonality, string length]
	:   (x=x_field {$referenceNumber = $x.referenceNumber;})
		(t=t_field {$title = $t.title;})
	    (m=m_field | l=l_field | q=q_field)*
		(k=k_field {
			$tonality = Tonality.parse($k.tonality_str);
			noteParser.setTonality($tonality);
		})
;

x_field returns [int referenceNumber]
	:	(x=XField	{ $referenceNumber = int.Parse(($x.text).Substring(2)); })
;

t_field returns [string title]
	:	(t=TField	{ $title = ($t.text).Substring(2); })
	|	//empty
;

m_field
	:	(m=MField)
;

q_field
	:	(q=QField)
;

l_field
	:	(LFieldHeader d=noteduration {noteParser.setUnitNoteLength($d.nd);})
;

k_field returns [string tonality_str]
	:	(k=KField	{ $tonality_str = ($k.text).Substring(2); })
	|	//empty	    { $tonality_str = "C"; })
;
	
score returns [Motif m]
	:	{ $m = new Motif(); $m.addVoice(new Voice());}
		(
			(me=music_elem {$m.getVoice(currentVoice).add($me.mi);})
			| (bar {noteParser.resetAccidentals();})
			| (ignored_elem)
			| (v=v_field {
					changeToVoice($v.voice_str, $m);
				})
		)*
	;
	
v_field returns [string voice_str]
	:	(v=VField	{ $voice_str = ($v.text).Substring(2); })
;
	
music_elem returns [MusicItem mi]
	:	(n=note {$mi = $n.n;})
	|	(c=chord {$mi = $c.c;})
;
	
ignored_elem
	:	WhiteSpace
	|	Remark
;
	
chord returns [Chord c]
	:	{$c = new Chord();}
		Lbrack (n=note {$c.add($n.n);})+ Rbrack
	;
	
note returns [Note n]
    :   (p=pitch nd=noteduration {$n = noteParser.parse($p.text, $nd.nd);})
	|	(p=pitch {$n = noteParser.parse($p.text, 1);})
    ;
	
noteduration returns [Duration nd]
	: 	(a=Num {$nd = int.Parse($a.text);})
	|	(a=Num '/' b=Num {$nd = new Duration(int.Parse($a.text), int.Parse($b.text));})
	|	('/' b=Num {$nd = new Duration(1, int.Parse($b.text));})
	;
	
pitch
	:	a=Accidental p=PitchSymbol
    |   p=PitchSymbol
    ;
	
bar : '|'
	| '|]'
	| '||'
	| '[|'
	| '|:'
	| ':|'
	| '::'
	;
	
/////////////////LEXER/////////////////////////

XField
	:	'X:' Num
;

TField
	:	'T:' (~[\n\r])*
;

VField
	:	'V:' (~[\n\r])*
;

KField
	:	'K:' (~[\n\r])*
;

MField
	:	'M:C'
	|	'M:C|'
	|	'M:' Num '/' Num
;

QField
	:	'Q:' (~[\n\r])*
;

LFieldHeader
	:	'L:'
;

Num		
	:	NumSymbol+
;
	
PitchSymbol
    :   ([A-G] | [a-g])   (',' | '\'')*
	|	'Z' | 'z'
;
	
Accidental
		:	'_'
		|	'__'
		|	'=' 
		|	'^'
		|	'^^'
;
		
Lbrack  : '[';
Rbrack  : ']';

NumSymbol 
	:   [0-9]
;

WhiteSpace
    :   [ \t]
;
	
Remark
    :   '[r:' .*? ']'
;

NewLine
    :   (   '\r' '\n'?
        |   '\n'
        )
		-> skip
;

LineComment
    :   '%' ~[\r\n]*
        -> skip
;