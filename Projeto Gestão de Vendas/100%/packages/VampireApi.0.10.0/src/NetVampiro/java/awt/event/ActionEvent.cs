/*
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at 
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *  
 */
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.eventj
{
    /**
     * @author Michael Danilov
     */
    [Serializable]
    public class ActionEvent : AWTEvent
    {

        private const long serialVersionUID = -7671078796273832149L;

        public const int SHIFT_MASK = 1;

        public const int CTRL_MASK = 2;

        public const int META_MASK = 4;

        public const int ALT_MASK = 8;

        public const int ACTION_FIRST = 1001;

        public const int ACTION_LAST = 1001;

        public const int ACTION_PERFORMED = 1001;

        private long when;
        private int modifiers;
        private String command;

        public ActionEvent(Object source, int id, String command) :
            this(source, id, command, 0)
        {
        }

        public ActionEvent(Object source, int id, String command, int modifiers) :
            this(source, id, command, 0l, modifiers)
        {
        }

        public ActionEvent(Object source, int id, String command, long when, int modifiers)
            : base(source, id)
        {
            this.command = command;
            this.when = when;
            this.modifiers = modifiers;
        }

        public int getModifiers()
        {
            return modifiers;
        }

        public String getActionCommand()
        {
            return command;
        }

        public long getWhen()
        {
            return when;
        }


        public String paramString()
        {
            /* The format is based on 1.5 release behavior 
             * which can be revealed by the following code:
             * 
             * ActionEvent e = new ActionEvent(new Component(){},
             *       ActionEvent.ACTION_PERFORMED, "Command",
             *       ActionEvent.SHIFT_MASK|ActionEvent.CTRL_MASK|
             *       ActionEvent.META_MASK|ActionEvent.ALT_MASK);
             * System.out.println(e);
             */

            String idString = (id == ACTION_PERFORMED) ?
                              "ACTION_PERFORMED" : "unknown type"; //$NON-NLS-1$ //$NON-NLS-2$
            String modifiersString = ""; //$NON-NLS-1$

            if ((modifiers & SHIFT_MASK) > 0)
            {
                modifiersString += "Shift"; //$NON-NLS-1$
            }
            if ((modifiers & CTRL_MASK) > 0)
            {
                modifiersString += modifiersString.length() == 0 ? "Ctrl" : "+Ctrl"; //$NON-NLS-1$ //$NON-NLS-2$
            }
            if ((modifiers & META_MASK) > 0)
            {
                modifiersString += modifiersString.length() == 0 ? "Meta" : "+Meta"; //$NON-NLS-1$ //$NON-NLS-2$
            }
            if ((modifiers & ALT_MASK) > 0)
            {
                modifiersString += modifiersString.length() == 0 ? "Alt" : "+Alt"; //$NON-NLS-1$ //$NON-NLS-2$
            }

            return (idString + ",cmd=" + command + ",when=" + when +  //$NON-NLS-1$ //$NON-NLS-2$
                    ",modifiers=" + modifiersString); //$NON-NLS-1$
        }

    }
}