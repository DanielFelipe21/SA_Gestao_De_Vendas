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
 *  Copyright © 2011, 2020 Sebastian Ritter
 */
using System;

namespace biz.ritter.javapi.lang
{
    /// <remarks>Author: Sebastian Ritter</remarks>
    /// Java 8 - missing
    /// Java 11 - missing
    /// Java 15 - implemented 
    public interface CharSequence
    {
        char charAt(int index);
        int length();
        CharSequence subSequence(int start, int end);
        String toString();
        
        
        /// see https://docs.oracle.com/en/java/javase/15/docs/api/java.base/java/lang/CharSequence.html#isEmpty()
        bool isEmpty() {
          return 0 == length();
        }
    }
}
