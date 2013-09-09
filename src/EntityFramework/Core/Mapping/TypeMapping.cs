// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Data.Entity.Core.Mapping
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Utilities;

    /// <summary>
    /// Represents the Mapping metadata for a type map in CS space.
    /// </summary>
    /// <example>
    /// For Example if conceptually you could represent the CS MSL file as following
    /// --Mapping
    /// --EntityContainerMapping ( CNorthwind-->SNorthwind )
    /// --EntitySetMapping
    /// --EntityTypeMapping
    /// --MappingFragment
    /// --EntityKey
    /// --ScalarPropertyMap
    /// --ScalarPropertyMap
    /// --EntityTypeMapping
    /// --MappingFragment
    /// --EntityKey
    /// --ScalarPropertyMap
    /// --ComplexPropertyMap
    /// --ScalarPropertyMap
    /// --ScalarProperyMap
    /// --ScalarPropertyMap
    /// --AssociationSetMapping
    /// --AssociationTypeMapping
    /// --MappingFragment
    /// --EndPropertyMap
    /// --ScalarPropertyMap
    /// --ScalarProperyMap
    /// --EndPropertyMap
    /// --ScalarPropertyMap
    /// This class represents the metadata for all the Type map elements in the
    /// above example namely EntityTypeMapping, AssociationTypeMapping and CompositionTypeMapping.
    /// The TypeMapping elements contain TableMappingFragments which in turn contain the property maps.
    /// </example>
    internal abstract class TypeMapping : MappingItem
    {
        /// <summary>
        /// Construct the new TypeMapping object.
        /// </summary>
        /// <param name="setMapping"> EntitySetBaseMapping that contains this type mapping </param>
        internal TypeMapping(EntitySetBaseMapping setMapping)
        {
            m_fragments = new List<MappingFragment>();
            m_setMapping = setMapping;
        }

        /// <summary>
        /// ExtentMap that contains this type mapping.
        /// </summary>
        private readonly EntitySetBaseMapping m_setMapping;

        /// <summary>
        /// Set of fragments that make up the type Mapping.
        /// </summary>
        private readonly List<MappingFragment> m_fragments;

        /// <summary>
        /// Mapping fragments that make up this set type
        /// </summary>
        public ReadOnlyCollection<MappingFragment> MappingFragments
        {
            get { return new ReadOnlyCollection<MappingFragment>(m_fragments); }
        }

        public EntitySetBaseMapping SetMapping
        {
            get { return m_setMapping; }
        }

        /// <summary>
        /// a list of TypeMetadata that this mapping holds true for.
        /// </summary>
        public abstract ReadOnlyCollection<EdmType> Types { get; }

        /// <summary>
        /// a list of TypeMetadatas for which the mapping holds true for
        /// not only the type specified but the sub-types of that type as well.
        /// </summary>
        public abstract ReadOnlyCollection<EdmType> IsOfTypes { get; }

        /// <summary>
        /// Add a fragment mapping as child of this type mapping
        /// </summary>
        public void AddFragment(MappingFragment fragment)
        {
            Check.NotNull(fragment, "fragment");

            m_fragments.Add(fragment);
        }

        internal void RemoveFragment(MappingFragment fragment)
        {
            DebugCheck.NotNull(fragment);

            m_fragments.Remove(fragment);
        }
    }
}