using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TensorCS.Core;
using Rubiks.Moves;

namespace Rubiks {
    public class RubiksCube {

        //U Move Permutation
        //               Up
        //           ╔══╦══╦══╗                         //           ╔══╦══╦══╗                   
        //           ║45│46│47║                         //           ║45│46│47║                    
        //           ║──┼──┼──║                         //           ║──┼──┼──║                   
        //           ║48│49│50║                         //           ║48│49│50║                   
        //           ║──┼──┼──║                         //           ║──┼──┼──║                   
        //     Left X║51│52│53║  Right    Back          //           ║51│52│53║                   
        //  ╔══╦══╦══╬══╬══╬══╬══╦══╦══╦══╦══╦══╗       //  ╔══╦══╦══╬══╬══╬══╬══╦══╦══╦══╦══╦══╗
        //  ║27│28│29║0 │1 │2 ║9 │10│11║18│19│20║       //  ║0 │1 │2 ║9 │10│11║18│19│20║27│28│29║
        //  ║──┼──┼──╬──┼──┼──╬──┼──┼──╬──┼──┼──║       //  ║──┼──┼──╬──┼──┼──╬──┼──┼──╬──┼──┼──║
        //  ║30│31│32║3 │4 |5 ║12│13│14║21│22│23║       //  ║30│31│32║3 │4 |5 ║12│13│14║21│22│23║
        //  ║──┼──┼──╬──┼──┼──╬──┼──┼──╬──┼──┼──║       //  ║──┼──┼──╬──┼──┼──╬──┼──┼──╬──┼──┼──║
        //  ║33│34│35║6 │7 │8 ║15│16│17║24│25│26║       //  ║33│34│35║6 │7 │8 ║15│16│17║24│25│26║
        //  ╚══╩══╩══╬══╬══╬══╬══╩══╩══╩══╩══╩══╝       //  ╚══╩══╩══╬══╬══╬══╬══╩══╩══╩══╩══╩══╝
        //         X ║36│37│38║                         //           ║36│37│38║                   
        //           ║──┼──┼──║                         //           ║──┼──┼──║                   
        //           ║39│40│41║                         //           ║39│40│41║                   
        //           ║──┼──┼──║                         //           ║──┼──┼──║                   
        //           ║42│43│44║                         //           ║42│43│44║                   
        //           ╚══╩══╩══╝                         //           ╚══╩══╩══╝                   
        //              Down




        //X Move Permutation
        //           ╔══╦══╦══╗                          //           ╔══╦══╦══╗                  
        //           ║45│46│47║                          //           ║0 │1 │2 ║                  
        //           ║──┼──┼──║                          //           ║──┼──┼──║                  
        //           ║48│49│50║                          //           ║3 │4 │5 ║                  
        //           ║──┼──┼──║                          //           ║──┼──┼──║                  
        //           ║51│52│53║                          //           ║6 │7 │8 ║                  
        //  ╔══╦══╦══╬══╬══╬══╬══╦══╦══╦══╦══╦══╗        //  ╔══╦══╦══╬══╬══╬══╬══╦══╦══╦══╦══╦══╗
        //  ║27│28│29║0 │1 │2 ║9 │10│11║18│19│20║        //  ║29│32│35║36│37│38║15│12│9 ║53│52|51║
        //  ║──┼──┼──╬──┼──┼──╬──┼──┼──╬──┼──┼──║        //  ║──┼──┼──╬──┼──┼──╬──┼──┼──╬──┼──┼──║
        //  ║30│31│32║3 │4 |5 ║12│13│14║21│22│23║        //  ║28│31│34║39│40|41║16│13│10║50│49│48║
        //  ║──┼──┼──╬──┼──┼──╬──┼──┼──╬──┼──┼──║        //  ║──┼──┼──╬──┼──┼──╬──┼──┼──╬──┼──┼──║
        //  ║33│34│35║6 │7 │8 ║15│16│17║24│25│26║        //  ║27│30│33║42│43│44║17│14│11║47│46│45║
        //  ╚══╩══╩══╬══╬══╬══╬══╩══╩══╩══╩══╩══╝        //  ╚══╩══╩══╬══╬══╬══╬══╩══╩══╩══╩══╩══╝
        //           ║36│37│38║                          //           ║26│25│24║                  
        //           ║──┼──┼──║                          //           ║──┼──┼──║                  
        //           ║39│40│41║                          //           ║23│22│21║                  
        //           ║──┼──┼──║                          //           ║──┼──┼──║                  
        //           ║42│43│44║                          //           ║20│19│18║                  
        //           ╚══╩══╩══╝                          //           ╚══╩══╩══╝                  



        //Y Move Per  mutation
        //           ╔══╦══╦══╗                          //          ╔══╦══╦══╗         
        //           ║45│46│47║                          //          ║51│48│45║         
        //           ║──┼──┼──║                          //          ║──┼──┼──║         
        //           ║48│49│50║                          //          ║52│49│46║         
        //           ║──┼──┼──║                          //          ║──┼──┼──║      
        //           ║51│52│53║                          //          ║53│50│47║
        //  ╔══╦══╦══╬══╬══╬══╬══╦══╦══╦══╦══╦══╗        // ╔══╦══╦══╬══╬══╬══╬══╦══╦══╦══╦══╦══╗
        //  ║27│28│29║0 │1 │2 ║9 │10│11║18│19│20║        // ║0 │1 │2 ║9 │10│11║18│19│20║27│28│29║
        //  ║──┼──┼──╬──┼──┼──╬──┼──┼──╬──┼──┼──║        // ║──┼──┼──╬──┼──┼──╬──┼──┼──╬──┼──┼──║
        //  ║30│31│32║3 │4 |5 ║12│13│14║21│22│23║        // ║3 │4 │5 ║12│13|14║21│22│23║30│31│32║
        //  ║──┼──┼──╬──┼──┼──╬──┼──┼──╬──┼──┼──║        // ║──┼──┼──╬──┼──┼──╬──┼──┼──╬──┼──┼──║
        //  ║33│34│35║6 │7 │8 ║15│16│17║24│25│26║        // ║6 │7 │8 ║15│16│17║24│25│26║33│34│35║
        //  ╚══╩══╩══╬══╬══╬══╬══╩══╩══╩══╩══╩══╝        // ╚══╩══╩══╬══╬══╬══╬══╩══╩══╩══╩══╩══╝
        //           ║36│37│38║                          //          ║38│41│44║         
        //           ║──┼──┼──║                          //          ║──┼──┼──║         
        //           ║39│40│41║                          //          ║37│40│43║         
        //           ║──┼──┼──║                          //          ║──┼──┼──║         
        //           ║42│43│44║                          //          ║36│39│42║         
        //           ╚══╩══╩══╝                          //          ╚══╩══╩══╝         

        public const int FaceLength = 3;

        public BaseTensor<RubiksColor> Data { get; internal set; }

        public RubiksCube() {
            this.Data = new EnumTensor<RubiksColor>(new Shape(6, FaceLength, FaceLength));
            RubiksColor[] faceColors = new RubiksColor[6] { RubiksColor.Green, RubiksColor.Red, RubiksColor.Blue, RubiksColor.Orange, RubiksColor.Yellow, RubiksColor.White };    


            for (int f = 0; f < 6; f++) {
                for (int x = 0; x < FaceLength; x++) {
                    for (int y = 0; y < FaceLength; y++) {
                        this.Data[f, y, x] = faceColors[f];
                    }
                }
            }
        }

        public IEnumerable<RubiksMove> Scramble(int moves = 30) {
            var random = new Random();
            var allMoves = Enum.GetValues<RubiksMove>().ToList();
            allMoves.Remove(RubiksMove.X);
            allMoves.Remove(RubiksMove.Y);
            allMoves.Remove(RubiksMove.Z); 
            allMoves.Remove(RubiksMove._X);
            allMoves.Remove(RubiksMove._Y);
            allMoves.Remove(RubiksMove._Z);
            for (int i = 0; i < moves; i++) {
                var move = allMoves[random.Next(allMoves.Count)];
                this.Move(move);
                yield return move;
            }
        }

        public void Move(RubiksMove move) {
            Rubiks.Moves.Move.GetMove(move).Apply(this);
        }

        public IEnumerable<RubiksMove> Move(string sequence) {
            RubiksMove[] moves = Rubiks.Moves.Move.Parse(sequence);
            foreach (var move in moves) {
                this.Move(move);
                yield return move;
            }
        }

        public RubiksColor[] GetFace(RubiksFace face) {
            List<RubiksColor> faceValues = new List<RubiksColor>();
            for (int x = 0; x < 3; x++) {
                for (int y = 0; y < 3; y++) {
                    faceValues.Add(this.Data[(int)face - 1, x, y]);
                }
            }
            return faceValues.ToArray();
        }

        public bool IsSolved() {
            bool solved = true;

            for (int f = 0; f < 6; f++) {
                var face = (RubiksFace)f + 1;
                var values = GetFace(face);
                if (values.Any(x => x != values[0])) {
                    solved = false;
                    break;
                }
            }

            return solved;
        }
    }
}
