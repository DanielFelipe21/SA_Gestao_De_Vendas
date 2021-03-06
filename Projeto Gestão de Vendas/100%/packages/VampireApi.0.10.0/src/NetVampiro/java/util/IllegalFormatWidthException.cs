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

namespace biz.ritter.javapi.util
{

    /**
     * An {@code IllegalFormatWidthException} will be thrown if the width is a
     * negative value other than -1 or in other cases where a width is not
     * supported.
     * 
     * @see java.lang.RuntimeException
     */
    [Serializable]
    public class IllegalFormatWidthException : IllegalFormatException
    {

        private const long serialVersionUID = 16660902L;

        private int w;

        /**
         * Constructs a new {@code IllegalFormatWidthException} with specified
         * width.
         * 
         * @param w
         *           the width.
         */
        public IllegalFormatWidthException(int w)
        {
            this.w = w;
        }

        /**
         * Returns the width associated with the exception.
         * 
         * @return the width.
         */
        public int getWidth()
        {
            return w;
        }

        /**
         * Returns the message of the exception.
         * 
         * @return the message of the exception.
         */

        public override String getMessage()
        {
            return java.lang.StringJ.valueOf(w);
        }
    }
}
