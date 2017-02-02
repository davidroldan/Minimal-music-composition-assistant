using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicaMinimalista.Objects.Music
{
    public enum NoteFigure
    {
        C = 0,
        D = 2,
        E = 4,
        F = 5,
        G = 7,
        A = 9,
        B = 11,

        CSharp = C + 1, CFlat = 11,
        DSharp = D + 1, DFlat = D - 1,
        ESharp = E + 1, EFlat = E - 1,
        FSharp = F + 1, FFlat = F - 1,
        GSharp = G + 1, GFlat = G - 1,
        ASharp = A + 1, AFlat = A - 1,
        BSharp = 0, BFlat = B - 1,

        Silence = -1
    }
}
