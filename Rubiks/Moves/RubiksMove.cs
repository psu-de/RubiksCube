using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubiks.Moves {
    public enum RubiksMove : byte {

        U = 1,
        D = 2, 
        R = 3, 
        L = 4,
        F = 5,  
        B = 6,  
        M = 7,   
        E = 8, 
        S = 9,  
        Rw = 10,    
        Lw = 11,    
        Uw = 12,    
        Dw = 13,   
        Fw = 14,    
        Bw = 15,
        X = 16,
        Y = 17,
        Z = 18,

        _U = U + 32,
        _D = D + 32,
        _R = R + 32,
        _L = L + 32,
        _F = F + 32,
        _B = B + 32,
        _M = M + 32,
        _E = E + 32,
        _S = S + 32,
        _Rw = Rw + 32,
        _Lw = Lw + 32,
        _Uw = Uw + 32,
        _Dw = Dw + 32,
        _Fw = Fw + 32,
        _Bw = Bw + 32,
        _X = X + 32,
        _Y = Y + 32,
        _Z = Z + 32

    }
}
