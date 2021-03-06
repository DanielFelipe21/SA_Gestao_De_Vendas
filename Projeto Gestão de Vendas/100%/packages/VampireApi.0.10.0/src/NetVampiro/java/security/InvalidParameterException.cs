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

namespace biz.ritter.javapi.security
{

    /**
     * {@code InvalidParameterException} indicates exceptional conditions, caused by
     * invalid parameters.
     */
    [Serializable]
    public class InvalidParameterException : java.lang.IllegalArgumentException
    {

        private static readonly long serialVersionUID = -857968536935667808L;

        /**
         * Constructs a new instance of {@code InvalidParameterException} with the
         * given message.
         *
         * @param msg
         *            the detail message for this exception.
         */
        public InvalidParameterException(String msg)
            : base(msg)
        {
        }

        /**
         * Constructs a new instance of {@code InvalidParameterException}.
         */
        public InvalidParameterException()
        {
        }
    }
}