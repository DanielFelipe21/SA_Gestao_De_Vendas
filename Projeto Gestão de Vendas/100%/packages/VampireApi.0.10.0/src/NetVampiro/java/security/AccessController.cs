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

namespace biz.ritter.javapi.security
{
    public sealed class AccessController
    {
        public static void checkPermission(Permission perm)
        {
            //TODO : implement it!
        }
        /**
         * Performs the privileged action specified by <code>action</code>.
         * 
         * When permission checks are made, if the permission has been granted by
         * all frames below and including the one representing the call to this
         * method, then the permission is granted. In otherwords, the check stops
         * here.
         * 
         * Any unchecked exception generated by this method will propagate up the
         * chain.
         * 
         * @param action
         *            the action being performed
         * @param <T>
         *            the return type for the privileged action
         * @return the result of evaluating the action
         * 
         * @see #doPrivileged(PrivilegedAction)
         */
        public static T doPrivileged<T>(PrivilegedAction<T> action)
        {
            return action.run();
        }
    }
}
