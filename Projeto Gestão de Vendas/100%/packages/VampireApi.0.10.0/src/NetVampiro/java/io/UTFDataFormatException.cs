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

namespace biz.ritter.javapi.io
{

    /**
     * Signals that an incorrectly encoded UTF-8 string has been encountered, most
     * likely while reading some {@link DataInputStream}.
     * 
     * @see DataInputStream#readUTF()
     */
    [Serializable]
    public class UTFDataFormatException : IOException {

        private static readonly long serialVersionUID = 420743449228280612L;

        /**
         * Constructs a new {@code UTFDataFormatException} with its stack trace
         * filled in.
         */
        public UTFDataFormatException() :base(){
        }

        /**
         * Constructs a new {@code UTFDataFormatException} with its stack trace and
         * detail message filled in.
         * 
         * @param detailMessage
         *            the detail message for this exception.
         */
        public UTFDataFormatException(String detailMessage) : base (detailMessage) {
        }
    }
}
