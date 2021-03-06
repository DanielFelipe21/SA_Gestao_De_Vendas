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

namespace biz.ritter.javapi.net
{

    /**
     * This class represents permissions to configure the access to network
     * resources.
     * <p>
     * There are three valid target names:
     * <dl>
     * <dt>setDefaultAuthenticator</dt>
     * <dd>Allows the default authenticator to be set.</dd>
     * <dt>requestPasswordAuthentication</dt>
     * <dd>Allows the default authenticator to be retrieved.</dd>
     * <dt>specifyStreamHandler</dt>
     * <dd>Allows a stream (protocol) handler to be set when constructing an URL
     * object</dd>
     * </dl>
     * 
     * @see java.security.BasicPermission
     * @see SecurityManager
     */
    [Serializable]
    public sealed class NetPermission : java.security.BasicPermission
    {

        private static readonly long serialVersionUID = -8343910153355041693L;

        /**
         * Creates an instance of this class with the given name.
         * 
         * @param name
         *            the name of the new NetPermission instance.
         */
        public NetPermission(String name)
            : base(name)
        {
        }

        /**
         * Creates an instance of this class with the given name and an action list.
         * The action list is ignored and should be {@code null}.
         * 
         * @param name
         *            the name of the new {@code NetPermission} instance.
         * @param actions
         *            the ignored action string.
         */
        public NetPermission(String name, String actions)
            : base(name, actions)
        {
        }
    }
}
