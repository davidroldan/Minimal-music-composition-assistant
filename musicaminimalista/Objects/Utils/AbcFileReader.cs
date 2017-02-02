using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using MusicaMinimalista.Objects;
using MusicaMinimalista.Objects.Music;

public class AbcFileReader
{
    public static Motif readFromFile(string filepath)
    {
        AntlrFileStream stream = new AntlrFileStream(filepath);
        AbcNotationLexer lexer = new AbcNotationLexer(stream);
        CommonTokenStream tokens = new CommonTokenStream(lexer);
        AbcNotationParser parser = new AbcNotationParser(tokens);
        return parser.file().m;
    }

    /*
7 sharps	    C#	A#m	    G#Mix	D#Dor	E#Phr	F#Lyd	B#Loc
6 sharps	    F#	D#m	    C#Mix	G#Dor	A#Phr	BLyd	E#Loc
5 sharps	    B	G#m 	F#Mix	C#Dor	D#Phr	ELyd	A#Loc
4 sharps	    E	C#m	    BMix	F#Dor	G#Phr	ALyd	D#Loc
3 sharps	    A	F#m	    EMix	BDor	C#Phr	DLyd	G#Loc
2 sharps	    D	Bm	    AMix	EDor	F#Phr	GLyd	C#Loc
1 sharp	        G	Em	    DMix	ADor	BPhr	CLyd	F#Loc
0 sharps/flats	C	Am	    GMix	DDor	EPhr	FLyd	BLoc
1 flat	        F	Dm	    CMix	GDor	APhr	BbLyd	ELoc
2 flats	        Bb	Gm	    FMix	CDor	DPhr	EbLyd	ALoc
3 flats	        Eb	Cm	    BbMix	FDor	GPhr	AbLyd	DLoc
4 flats	        Ab	Fm	    EbMix	BbDor	CPhr	DbLyd	GLoc
5 flats	        Db	Bbm	    AbMix	EbDor	FPhr	GbLyd	CLoc
6 flats	        Gb	Ebm	    DbMix	AbDor	BbPhr	CbLyd	FLoc
7 flats	        Cb	Abm	    GbMix	DbDor	EbPhr	FbLyd	BbLoc
     */
}