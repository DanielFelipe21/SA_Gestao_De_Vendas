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
public class InputMethodEvent : AWTEvent {

    private const long serialVersionUID = 4727190874778922661L;

    public const int INPUT_METHOD_FIRST = 1100;

    public const int INPUT_METHOD_TEXT_CHANGED = 1100;

    public const int CARET_POSITION_CHANGED = 1101;

    public const int INPUT_METHOD_LAST = 1101;

    private java.text.AttributedCharacterIterator text;
    private java.awt.font.TextHitInfo visiblePosition;
    private java.awt.font.TextHitInfo caret;
    private int committedCharacterCount;
    private long when;

    public InputMethodEvent(Component src, int id,
                            java.awt.font.TextHitInfo caret,
                            java.awt.font.TextHitInfo visiblePos) :
        this(src, id, null, 0, caret, visiblePos){
    }

    public InputMethodEvent(Component src, int id, 
                            java.text.AttributedCharacterIterator text,
                            int commitedCharCount,
                            java.awt.font.TextHitInfo caret,
                            java.awt.font.TextHitInfo visiblePos) :
        this(src, id, 0l, text, commitedCharCount, caret, visiblePos){
    }

    public InputMethodEvent(Component src, int id, long when,
                            java.text.AttributedCharacterIterator text, 
                            int committedCharacterCount,
                            java.awt.font.TextHitInfo caret,
                            java.awt.font.TextHitInfo visiblePos) :
        base(src, id){

        if ((id < INPUT_METHOD_FIRST) || (id > INPUT_METHOD_LAST)) {
            // awt.18E=Wrong event id
            throw new java.lang.IllegalArgumentException("Wrong event id"); //$NON-NLS-1$
        }
        if ((id == CARET_POSITION_CHANGED) && (text != null)) {
            // awt.18F=Text must be null for CARET_POSITION_CHANGED
            throw new java.lang.IllegalArgumentException("Text must be null for CARET_POSITION_CHANGED"); //$NON-NLS-1$
        }
        if ((text != null) &&
                ((committedCharacterCount < 0) ||
                 (committedCharacterCount > 
                        (text.getEndIndex() - text.getBeginIndex())))) {
            // awt.190=Wrong committedCharacterCount
            throw new java.lang.IllegalArgumentException("Wrong committedCharacterCount"); //$NON-NLS-1$
        }

        this.when = when;
        this.text = text;
        this.caret = caret;
        this.visiblePosition = visiblePos;
        this.committedCharacterCount = committedCharacterCount;
    }

    public java.awt.font.TextHitInfo getCaret()
    {
        return caret;
    }

    public int getCommittedCharacterCount() {
        return committedCharacterCount;
    }

    public java.text.AttributedCharacterIterator getText() {
        return text;
    }

    public java.awt.font.TextHitInfo getVisiblePosition()
    {
        return visiblePosition;
    }

    public long getWhen() {
        return when;
    }

    
    public override void consume() {
        base.consume();
    }

    
    public override bool isConsumed() {
        return base.isConsumed();
    }

    
    public String paramString() {
        /* The format is based on 1.5 release behavior 
         * which can be revealed by the following code:
         * 
         * InputMethodEvent e = new InputMethodEvent(new Component(){},
         *          InputMethodEvent.INPUT_METHOD_TEXT_CHANGED,
         *          TextHitInfo.leading(1), TextHitInfo.trailing(2));
         * System.out.println(e);
         */
        String typeString = null;

        switch (id) {
        case INPUT_METHOD_TEXT_CHANGED:
            typeString = "INPUT_METHOD_TEXT_CHANGED"; //$NON-NLS-1$
            break;
        case CARET_POSITION_CHANGED:
            typeString = "CARET_POSITION_CHANGED"; //$NON-NLS-1$
            break;
        default:
            typeString = "unknown type"; //$NON-NLS-1$
                break;
        }

        return typeString + ",text=" + text +  //$NON-NLS-1$
                ",commitedCharCount=" + committedCharacterCount + //$NON-NLS-1$
                ",caret=" + caret + ",visiblePosition=" + visiblePosition; //$NON-NLS-1$ //$NON-NLS-2$
    }

}
}