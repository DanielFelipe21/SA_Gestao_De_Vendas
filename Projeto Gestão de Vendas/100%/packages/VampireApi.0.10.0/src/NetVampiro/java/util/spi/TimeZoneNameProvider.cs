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

namespace biz.ritter.javapi.util.spi
{

    /**
     * TimeZoneNameProvider is an abstract class to get localized time zone names
     * from service providers.
     * 
     * @since 1.6
     * 
     */
    public abstract class TimeZoneNameProvider : LocaleServiceProvider
    {

        /**
         * The constructor
         * 
         */
        protected TimeZoneNameProvider()
        {
            // do nothing
        }

        /**
         * Gets the name of the specified time zone ID that's suitable to display to
         * the user.
         * 
         * @param id
         *            id of time zone
         * @param daylight
         *            true to return the daylight saving time.
         * @param style
         *            TimeZone.LONG or TimeZone.SHORT
         * @param locale
         *            the locale
         * @return the readable time zone name, or null if it is unavailable
         * @throws NullPointerException
         *             if id or locale is null
         * @throws IllegalArgumentException
         *             if locale is not available or style is invalid
         */
        public abstract String getDisplayName(String id, bool daylight,
                int style, Locale locale);
    }
}