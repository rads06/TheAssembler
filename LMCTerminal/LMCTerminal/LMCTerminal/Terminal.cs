using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LMCTerminal
{
    public class Terminal : ITerminal
    {
        SpriteFont font;
        Texture2D background;
        int cursorIndex;
        StringBuilder buffer;
        StringBuilder reportBuffer;
        Vector2 offset;
        LittleMan littleMan;
        Shell shell;
        string prompt;

        public string Prompt 
        {
            get
            {
                return prompt;
            }
            set
            {
                prompt = value;
            }
        }

        int cursorTimer;
        bool cursorOn;
        public string Cursor
        {
            get
            {
                if (cursorTimer == 0)
                {
                    cursorOn ^= true;
                    cursorTimer = 50;
                }
                cursorTimer--;

                return (cursorOn) ? "|" : "";
            }
        }

        public Terminal(SpriteFont Font, Texture2D Background, string Prompt = "")
        {
            cursorTimer = 0;
            cursorOn = false;
            cursorIndex = 0;

            littleMan = new LittleMan(this);
            shell = new Shell(this);
            this.font = Font;
            this.background = Background;
            this.Prompt = Prompt;
            buffer = new StringBuilder();
            reportBuffer = new StringBuilder();
            this.Clear();
            offset = new Vector2(50, 50);
        }

        public string[] Buffer
        {
            get
            {
                return buffer.ToString().Split(new String[] { Environment.NewLine }, StringSplitOptions.None);
            }
        }
        public void Clear() 
        { 
            buffer.Clear();
            reportBuffer.Clear();
            cursorIndex = 0;
        }
        public void Write(string text) 
        { 
            buffer.Append(text);
            cursorIndex += text.Length;
        }
        public void WriteLine(string text) 
        { 
            buffer.AppendLine(text);
            cursorIndex += text.Length + Environment.NewLine.Length;
        }
        public void ReportWrite(string text)
        {
            reportBuffer.Append(text);           
        }
        public void ReportLine(string text)
        {
            reportBuffer.AppendLine(text);
        }
        public IEnumerable<string> Registers { get { return littleMan.Registers; } }
        public void RunProgram(IEnumerable<string> buffer) { littleMan.run(buffer); }

        //ITerminal Implementation
        IEnumerable<string> ITerminal.Buffer { get { return this.Buffer; } }
        IEnumerable<string> ITerminal.Registers { get { return this.Registers; } }
        void ITerminal.Clear() { this.Clear(); }
        void ITerminal.Write(string text) { this.Write(text); }
        void ITerminal.WriteLine(string text) { this.WriteLine(text); }
        void ITerminal.ReportWrite(string text) { this.ReportWrite(text); }
        void ITerminal.ReportLine(string text) { this.ReportLine(text); }
        void ITerminal.RunProgram(IEnumerable<string> buffer) { this.RunProgram(buffer); }

        public void Update()
        {
            KeyboardExt.Update();
            shell.Update();
            char[] pressedLetters = KeyboardExt.GetPressedAlphas();
            buffer.Append(pressedLetters);
            cursorIndex += pressedLetters.Count();

            if (KeyboardExt.IsKeyJustPressed(Keys.Space))
            {
                buffer.Append(' ');
                cursorIndex++;
            }
            else if (KeyboardExt.IsKeyJustPressed(Keys.Enter))
            {
                if (shell.IsInEditMode)
                {
                    buffer.Append(Environment.NewLine);
                    cursorIndex += Environment.NewLine.Length;
                }
                else
                {
                    if (buffer.Length > 0)
                        shell.ParseCommand(buffer.ToString());
                }
            }
            else if (KeyboardExt.IsKeyJustPressed(Keys.Back) && cursorIndex > 0)
            {
                buffer.Remove(cursorIndex-1, 1);
                cursorIndex--;
            }
            else if (KeyboardExt.IsKeyJustPressed(Keys.F1) && shell.IsInEditMode)
            {
                shell.SaveProgram();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(font, BufferToString(), offset, Color.GreenYellow);
            sb.Draw(background, Vector2.Zero, Color.White);
        }

        public string BufferToString()
        {
            StringBuilder repbuf = new StringBuilder(reportBuffer.ToString());
            StringBuilder buf = new StringBuilder(buffer.ToString());
            buf.Insert(cursorIndex, Cursor);
            if (!shell.IsInEditMode)
                buf.Insert(0, Prompt);
            //repbuf.Append(buf.ToString());
            return repbuf.ToString() + buf.ToString();
        }
    }
}