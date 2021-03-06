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
 */
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.eventj
{
    /**
     * @author Michael Danilov
     */
    [Serializable]
    public class HierarchyEvent : AWTEvent
    {

        private const long serialVersionUID = -5337576970038043990L;

        public const int HIERARCHY_FIRST = 1400;

        public const int HIERARCHY_CHANGED = 1400;

        public const int ANCESTOR_MOVED = 1401;

        public const int ANCESTOR_RESIZED = 1402;

        public const int HIERARCHY_LAST = 1402;

        public const int PARENT_CHANGED = 1;

        public const int DISPLAYABILITY_CHANGED = 2;

        public const int SHOWING_CHANGED = 4;

        private Container changedParent;
        private Component changed;
        private long changeFlag;

        public HierarchyEvent(Component source, int id, Component changed,
                              Container changedParent) :
            this(source, id, changed, changedParent, 0l)
        {
        }

        public HierarchyEvent(Component source, int id, Component changed,
                              Container changedParent, long changeFlags) :
            base(source, id)
        {

            this.changed = changed;
            this.changedParent = changedParent;
            this.changeFlag = changeFlags;
        }

        public virtual Component getComponent()
        {
            return (Component)source;
        }

        public virtual long getChangeFlags()
        {
            return changeFlag;
        }

        public virtual Component getChanged()
        {
            return changed;
        }

        public virtual Container getChangedParent()
        {
            return changedParent;
        }


        public String paramString()
        {
            /* The format is based on 1.5 release behavior 
             * which can be revealed by the following code:
             * 
             * HierarchyEvent e = new HierarchyEvent(new Button("Button"),
             *          HierarchyEvent.HIERARCHY_CHANGED,
             *          new Panel(), new Container());
             * System.out.println(e);
             */
            String paramString = null;

            switch (id)
            {
                case HIERARCHY_CHANGED:
                    paramString = "HIERARCHY_CHANGED"; //$NON-NLS-1$
                    break;
                case ANCESTOR_MOVED:
                    paramString = "ANCESTOR_MOVED"; //$NON-NLS-1$
                    break;
                case ANCESTOR_RESIZED:
                    paramString = "ANCESTOR_RESIZED"; //$NON-NLS-1$
                    break;
                default:
                    paramString = "unknown type"; //$NON-NLS-1$
                    break;
            }

            paramString += " ("; //$NON-NLS-1$

            if (id == HIERARCHY_CHANGED)
            {
                if ((changeFlag & PARENT_CHANGED) > 0)
                {
                    paramString += "PARENT_CHANGED,"; //$NON-NLS-1$
                }
                if ((changeFlag & DISPLAYABILITY_CHANGED) > 0)
                {
                    paramString += "DISPLAYABILITY_CHANGED,"; //$NON-NLS-1$
                }
                if ((changeFlag & SHOWING_CHANGED) > 0)
                {
                    paramString += "SHOWING_CHANGED,"; //$NON-NLS-1$
                }
            }

            return paramString + "changed=" + changed +  //$NON-NLS-1$
                    ",changedParent=" + changedParent + ")"; //$NON-NLS-1$ //$NON-NLS-2$
        }

    }
}