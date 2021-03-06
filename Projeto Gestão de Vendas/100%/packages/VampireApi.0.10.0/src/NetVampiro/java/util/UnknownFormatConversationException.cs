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
using biz.ritter.javapi;

namespace biz.ritter.javapi.util
{

    /**
     * An {@code UnknownFormatConversionException} will be thrown if the format
     * conversion is unknown.
     * 
     * @see java.lang.RuntimeException
     */
    [Serializable]
    public class UnknownFormatConversionException : IllegalFormatException {
        private static readonly long serialVersionUID = 19060418L;

        private String s;

        /**
         * Constructs an {@code UnknownFormatConversionException} with the unknown
         * format conversion.
         * 
         * @param s 
         *           the unknown format conversion.
         */
        public UnknownFormatConversionException(String s) {
            if( null == s ) {
                throw new java.lang.NullPointerException();
            }
            this.s = s;
        }

        /**
         * Returns the conversion associated with the exception.
         * 
         * @return the conversion associated with the exception.
         */
        public String getConversion() {
            return s;
        }

        /**
         * Returns the message of the exception.
         * 
         * @return the message of the exception.
         */
        
        public override String getMessage() {
            return "Conversion is '"+ s+"'";
        }
    }
}
