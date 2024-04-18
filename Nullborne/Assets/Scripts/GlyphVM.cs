using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Instruction : byte
{
    INST_LITERAL = 0x00,
    // INST_NAME = 0x01,
}



public class GlyphVM : MonoBehaviour
{

    Stack<byte> stack_ = new Stack<byte>();



    // executes the bytecode as it is passed in
    public void interpret(byte[] bytecode)
    {

        foreach(byte code in bytecode)
        {
            if(code == (byte)Instruction.INST_LITERAL)
            {
                stack_.Push(code);
                continue;
            }

            Execute((Instruction)code);
        }

    }



    private void Execute(Instruction opcode)
    {

        switch(opcode)
        {
            
        }

    }

}