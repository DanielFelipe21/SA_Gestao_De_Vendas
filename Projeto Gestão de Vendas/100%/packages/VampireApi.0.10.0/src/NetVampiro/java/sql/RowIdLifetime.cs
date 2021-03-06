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

namespace biz.ritter.javapi.sql
{
    /**
     * An enumeration to describe the life-time of RowID.
     * 
     * @since 1.6
     */
    public enum RowIdLifetime
    {
        ROWID_UNSUPPORTED, ROWID_VALID_OTHER, ROWID_VALID_SESSION, ROWID_VALID_TRANSACTION, ROWID_VALID_FOREVER,
    }
}