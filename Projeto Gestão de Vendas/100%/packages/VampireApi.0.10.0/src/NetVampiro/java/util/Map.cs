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

namespace biz.ritter.javapi.util
{

    /// <summary>
    /// A <code>Map</code> is a data structure consisting of a set of keys and values
    /// in which each key is mapped to a single value.  The class of the objects
    /// used as keys is declared when the <code>Map</code> is declared, as is the 
    /// class of the corresponding values.
    /// <p />
    /// A <code>Map</code> provides helper methods to iterate through all of the
    /// keys contained in it, as well as various methods to access and update 
    /// the key/value pairs.  
    /// </summary>
    /// <typeparam name="K">key type</typeparam>
    /// <typeparam name="V">value type</typeparam>
    public interface Map<K,V> {

        /**
         * Removes all elements from this {@code Map}, leaving it empty.
         * 
         * @throws UnsupportedOperationException
         *                if removing elements from this {@code Map} is not supported.
         * @see #isEmpty()
         * @see #size()
         */
        void clear();

        /**
         * Returns whether this {@code Map} contains the specified key.
         * 
         * @param key
         *            the key to search for.
         * @return {@code true} if this map contains the specified key,
         *         {@code false} otherwise.
         */
        bool containsKey(Object key);

        /**
         * Returns whether this {@code Map} contains the specified value.
         * 
         * @param value
         *            the value to search for.
         * @return {@code true} if this map contains the specified value,
         *         {@code false} otherwise.
         */
        bool containsValue(Object value);

        /**
         * Returns a {@code Set} containing all of the mappings in this {@code Map}. Each mapping is
         * an instance of {@link Map.Entry}. As the {@code Set} is backed by this {@code Map},
         * changes in one will be reflected in the other.
         * 
         * @return a set of the mappings
         */
        Set<MapNS.Entry<K,V>> entrySet();

        /**
         * Compares the argument to the receiver, and returns {@code true} if the
         * specified object is a {@code Map} and both {@code Map}s contain the same mappings.
         * 
         * @param object
         *            the {@code Object} to compare with this {@code Object}.
         * @return boolean {@code true} if the {@code Object} is the same as this {@code Object}
         *         {@code false} if it is different from this {@code Object}.
         * @see #hashCode()
         * @see #entrySet()
         *
        bool equals(Object obj);
         */

        /**
         * Returns the value of the mapping with the specified key.
         * 
         * @param key
         *            the key.
         * @return the value of the mapping with the specified key, or {@code null}
         *         if no mapping for the specified key is found.
         */
        V get(Object key);

        /**
         * Returns an integer hash code for the receiver. {@code Object}s which are equal
         * return the same value for this method.
         * 
         * @return the receiver's hash.
         * @see #equals(Object)
         *
        int hashCode();
         */

        /**
         * Returns whether this map is empty.
         * 
         * @return {@code true} if this map has no elements, {@code false}
         *         otherwise.
         * @see #size()
         */
        bool isEmpty();

        /**
         * Returns a set of the keys contained in this {@code Map}. The {@code Set} is backed by
         * this {@code Map} so changes to one are reflected by the other. The {@code Set} does not
         * support adding.
         * 
         * @return a set of the keys.
         */
        Set<K> keySet();

        /**
         * Maps the specified key to the specified value.
         * 
         * @param key
         *            the key.
         * @param value
         *            the value.
         * @return the value of any previous mapping with the specified key or
         *         {@code null} if there was no mapping.
         * @throws UnsupportedOperationException
         *                if adding to this {@code Map} is not supported.
         * @throws ClassCastException
         *                if the class of the key or value is inappropriate for
         *                this {@code Map}.
         * @throws IllegalArgumentException
         *                if the key or value cannot be added to this {@code Map}.
         * @throws NullPointerException
         *                if the key or value is {@code null} and this {@code Map} does
         *                not support {@code null} keys or values.
         */
        V put(K key, V value);

        /**
         * Copies every mapping in the specified {@code Map} to this {@code Map}.
         * 
         * @param map
         *            the {@code Map} to copy mappings from.
         * @throws UnsupportedOperationException
         *                if adding to this {@code Map} is not supported.
         * @throws ClassCastException
         *                if the class of a key or a value of the specified {@code Map} is
         *                inappropriate for this {@code Map}.
         * @throws IllegalArgumentException
         *                if a key or value cannot be added to this {@code Map}.
         * @throws NullPointerException
         *                if a key or value is {@code null} and this {@code Map} does not
         *                support {@code null} keys or values.
         */
        void putAll(Map<K,V> map);

        /**
         * Removes a mapping with the specified key from this {@code Map}.
         * 
         * @param key
         *            the key of the mapping to remove.
         * @return the value of the removed mapping or {@code null} if no mapping
         *         for the specified key was found.
         * @throws UnsupportedOperationException
         *                if removing from this {@code Map} is not supported.
         */
        V remove(Object key);

        /**
         * Returns the number of mappings in this {@code Map}.
         * 
         * @return the number of mappings in this {@code Map}.
         */
        int size();

        /**
         * Returns a {@code Collection} of the values contained in this {@code Map}. The {@code Collection}
         * is backed by this {@code Map} so changes to one are reflected by the other. The
         * {@code Collection} supports {@link Collection#remove}, {@link Collection#removeAll}, 
         * {@link Collection#retainAll}, and {@link Collection#clear} operations,
         * and it does not support {@link Collection#add} or {@link Collection#addAll} operations.
         * <p>
         * This method returns a {@code Collection} which is the subclass of
         * {@link AbstractCollection}. The {@link AbstractCollection#iterator} method of this subclass returns a
         * "wrapper object" over the iterator of this {@code Map}'s {@link #entrySet()}. The {@link AbstractCollection#size} method
         * wraps this {@code Map}'s {@link #size} method and the {@link AbstractCollection#contains} method wraps this {@code Map}'s
         * {@link #containsValue} method.
         * <p>
         * The collection is created when this method is called at first time and
         * returned in response to all subsequent calls. This method may return
         * different Collection when multiple calls to this method, since it has no
         * synchronization performed.
         * 
         * @return a collection of the values contained in this map.
         */
        Collection<V> values();
    }
    
    namespace MapNS {
        /**
         * {@code Map.Entry} is a key/value mapping contained in a {@code Map}.
         */
        public interface Entry<K, V>
        {
            /**
             * Returns the key.
             * 
             * @return the key
             */
            K getKey();

            /**
             * Returns the value.
             * 
             * @return the value
             */
            V getValue();

            /**
             * Returns an integer hash code for the receiver. {@code Object} which are
             * equal return the same value for this method.
             * 
             * @return the receiver's hash code.
             * @see #equals(Object)
             *
            int hashCode();
             */

            /**
             * Sets the value of this entry to the specified value, replacing any
             * existing value.
             * 
             * @param object
             *            the new value to set.
             * @return object the replaced value of this entry.
             */
            V setValue(V obj);
        };
    }
}
