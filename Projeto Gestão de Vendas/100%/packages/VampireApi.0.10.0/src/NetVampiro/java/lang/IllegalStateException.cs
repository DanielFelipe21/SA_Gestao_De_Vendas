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
     * Thrown when an action is attempted at a time when the virtual machine is not
     * in the correct state.
     */
    /// <remarks>Class is ported from Apache Harmony project.</remarks>
    [Serializable]
    public class IllegalStateException : java.lang.RuntimeException {

        private static readonly long serialVersionUID = -1848914673093119416L;

        /**
         * Constructs a new {@code IllegalStateException} that includes the current
         * stack trace.
         */
        public IllegalStateException() :base () {
        }

        /**
         * Constructs a new {@code IllegalStateException} with the current stack
         * trace and the specified detail message.
         * 
         * @param detailMessage
         *            the detail message for this exception.
         */
        public IllegalStateException(String detailMessage) : base (detailMessage) {
        }
    
        /**
         * Constructs a new {@code IllegalStateException} with the current stack
         * trace, the specified detail message and the specified cause.
         * 
         * @param message
         *            the detail message for this exception.
         * @param cause
         *            the cause of this exception.
         * @since 1.5
         */
        public IllegalStateException(String message, Throwable cause) : base (message,cause) {
        }
    
        /**
         * Constructs a new {@code IllegalStateException} with the current stack
         * trace and the specified cause.
         * 
         * @param cause
         *            the cause of this exception, may be {@code null}.
         * @since 1.5
         */
        public IllegalStateException(Throwable cause) : base ((cause == null ? null : cause.toString()), cause) {}
    }
}
