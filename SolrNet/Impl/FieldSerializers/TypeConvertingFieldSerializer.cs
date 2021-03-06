﻿#region license
// Copyright (c) 2007-2010 Mauricio Scheffer
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SolrNet.Impl.FieldSerializers {
    /// <summary>
    /// Serializes using <see cref="TypeConverter"/>s
    /// </summary>
    public class TypeConvertingFieldSerializer : ISolrFieldSerializer {
        /// <inheritdoc />
        public bool CanHandleType(Type t) {
            var converter = TypeDescriptor.GetConverter(t);
            return converter.CanConvertTo(typeof (string));
        }

        /// <inheritdoc />
        public IEnumerable<PropertyNode> Serialize(object obj) {
            if (obj == null)
                yield break;
            var converter = TypeDescriptor.GetConverter(obj.GetType());
            yield return new PropertyNode {
                FieldValue = converter.ConvertToInvariantString(obj)
            };
        }
    }
}
