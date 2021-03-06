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

namespace biz.ritter.javapi.net
{
    /// <summary>
    ///Defines a factory which creates an {@code URLStreamHandler} for a specified
    ///protocol. It is used by the class {@code URL}.
    /// </summary>
    public interface URLStreamHandlerFactory
    {

        /**
         * Creates a new {@code URLStreamHandler} instance for the given {@code
         * protocol}.
         * 
         * @param protocol
         *            the protocol for which a handler is needed.
         * @return the created handler.
         */
        URLStreamHandler createURLStreamHandler(String protocol);
    }
}