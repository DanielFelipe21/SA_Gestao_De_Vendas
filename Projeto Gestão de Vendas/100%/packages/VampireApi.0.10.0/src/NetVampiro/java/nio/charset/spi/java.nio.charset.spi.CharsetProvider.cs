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

namespace biz.ritter.javapi.nio.charset.spi
{

    /**
     * The service provider class for character sets.
     */
    public abstract class CharsetProvider {

        // The permission required to construct a new provider.
        //private static RuntimePermission CONSTRUCT_PERM = new RuntimePermission("charsetProvider"); //$NON-NLS-1$

        /**
         * Constructor for subclassing with concrete types.
         * 
         * @throws SecurityException
         *             if there is a security manager installed that does not permit
         *             the runtime permission labeled "charsetProvider".
         */
        protected CharsetProvider() {
            /*SecurityManager securityManager = System.getSecurityManager();
            if (securityManager != null)
                securityManager.checkPermission(CONSTRUCT_PERM);*/
        }

        /**
         * Returns an iterator over all the available charsets.
         * 
         * @return the iterator.
         */
        public abstract java.util.Iterator<java.nio.charset.Charset> charsets();

        /**
         * Returns the named charset.
         * <p>
         * If the charset is unavailable the method returns <code>null</code>.
         * 
         * @param charsetName
         *            the canonical or alias name of a character set.
         * @return the charset, or <code>null</code> if unavailable.
         */
        public abstract java.nio.charset.Charset charsetForName(String charsetName);
    }
}
