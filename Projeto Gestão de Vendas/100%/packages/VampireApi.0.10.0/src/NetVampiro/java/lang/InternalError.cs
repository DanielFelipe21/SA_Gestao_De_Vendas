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

namespace biz.ritter.javapi.lang
{
    /**
     * Thrown when the virtual machine notices that it has gotten into an undefined
     * state.
     */
    public class InternalError : VirtualMachineError {

        private static readonly long serialVersionUID = -9062593416125562365L;

        /**
         * Constructs a new {@code InternalError} that includes the current stack
         * trace.
         */
        public InternalError() : base (){
        }

        /**
         * Constructs a new {@code InternalError} with the current stack trace and
         * the specified detail message.
         * 
         * @param detailMessage
         *            the detail message for this error.
         */
        public InternalError(String detailMessage) :base (detailMessage) {
        }
    }
}
