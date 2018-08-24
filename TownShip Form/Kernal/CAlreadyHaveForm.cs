using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TownShip_Form.Kernal
{
    class CAlreadyHaveForm : Form
    {
        CSmartArray m_AlreadyHaveArray;
        CAllProductSpisok m_Spisok = new CAllProductSpisok();
        List<TextBox> TextList = new List<TextBox>();
        List<NumericUpDown> NumericList = new List<NumericUpDown>();
        public CAlreadyHaveForm(CSmartArray array)
        {
            m_AlreadyHaveArray = array;
            int count = m_Spisok.GetCount();
            List<string> nameList = new List<string>();
            for (int i = 0; i < count; i++)
                nameList.Add(m_Spisok.GetName((ProductTag)i+1));

            nameList.Sort();

            int boxWidht = 100;
            int controlHeight = 21;

            int spaceBoxNumeric = 10;

            int numericWidht = 30;

            int newLineSpace = 10;

            int controlsInColumn = 12;

            int spaceColumn = 10;

            int columnFullWidth = boxWidht + spaceBoxNumeric + numericWidht;
            int columnFullHeight = controlHeight * controlsInColumn + newLineSpace * (controlsInColumn - 1);

            int startX = 10;
            int startY = 10;
            int X = startX;
            int Y = startY;
            int maxColumn = 1;
            for (int i = 0; i < count; i++)
            {
                TextBox box = new TextBox();
                box.Enabled = false;
                box.Size = new System.Drawing.Size(boxWidht, controlHeight);
                box.Location = new System.Drawing.Point(X, Y);
                box.Text = nameList[i];
                TextList.Add(box);
                Controls.Add(box);

                NumericUpDown numeric = new NumericUpDown();
                numeric.Size = new System.Drawing.Size(numericWidht, controlHeight);
                numeric.Location = new System.Drawing.Point(X + boxWidht + spaceBoxNumeric, Y);
                numeric.Value = array.GetTagCount(nameList[i]);
                NumericList.Add(numeric);
                Controls.Add(numeric);
                Y += controlHeight + newLineSpace;
                if ((i+1) % controlsInColumn == 0)
                {
                    X += columnFullWidth + spaceColumn;
                    Y = startY;
                    maxColumn++;
                }
            }
            int FormHeight = columnFullHeight + 2 * startY;
            int FormWidht = columnFullWidth * maxColumn + spaceColumn * (maxColumn - 1) + 2 * startX;
            ClientSize = new System.Drawing.Size(FormWidht, FormHeight);
        }

        public CSmartArray GetArray()
        {
            CSmartArray array = new CSmartArray();
            int count = TextList.Count;
            for (int i = 0; i < count; i++)
            {
                array.Add(TextList[i].Text, (int)NumericList[i].Value);
            }
            return array;
        }
    }
}
