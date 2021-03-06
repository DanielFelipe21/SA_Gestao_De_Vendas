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

namespace biz.ritter.javapi.net
{

    /**
     * This {@code SocketException} may be thrown during socket creation or setting
     * options, and is the superclass of all other socket related exceptions.
     */
    public class SocketException : java.io.IOException {

        private static readonly long serialVersionUID = -5935874303556886934L;

        /**
         * Constructs a new {@code SocketException} instance with its walkback
         * filled in.
         */
        public SocketException() :base (){
        }

        /**
         * Constructs a new {@code SocketException} instance with its walkback and
         * message filled in.
         * 
         * @param detailMessage
         *            the detail message of this exception.
         */
        public SocketException(String detailMessage) :base (detailMessage) {
        }
    }
}
