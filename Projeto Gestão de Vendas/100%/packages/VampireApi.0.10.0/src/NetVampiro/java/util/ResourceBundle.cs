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
 *  Copyright © 2011 Sebastian Ritter
 */

using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.util
{
    public abstract class ResourceBundle
    {
        public abstract java.util.Enumeration<Object> getKeys();
        public Object getObject(String key)
        {
            return handleGetObject(key);
        }
        protected abstract Object handleGetObject(String key);

        public static ResourceBundle getBundle(String name)
        {
            return getBundle(name, Locale.getDefault(), java.lang.ClassLoader.getSystemClassLoader());
        }
        public static ResourceBundle getBundle(String name, Locale locale, java.lang.ClassLoader classLoader)
        {
            throw new java.lang.UnsupportedOperationException("Not yet implemented");
        }
        public String getString(String name)
        {
            return handleGetObject(name).ToString();
        }
        public abstract java.util.Locale getLocale();
    }

}
