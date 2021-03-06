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

using java = biz.ritter.javapi;

namespace biz.ritter.javapi.lang
{

    /**
     * Objects of classes that implement this interface can be used within a
     * {@code foreach} statement.
     *
     * @since 1.5
     */
    public interface Iterable<T> {

        /**
         * Returns an {@link Iterator} for the elements in this object.
         * 
         * @return An {@code Iterator} instance.
         */
        java.util.Iterator<T> iterator();
    }
}
