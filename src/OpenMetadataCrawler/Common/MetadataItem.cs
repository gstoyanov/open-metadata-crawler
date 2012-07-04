using System;
using System.Diagnostics;

namespace OpenMetadataCrawler
{
    /// <summary>
    /// Contains a single metadata entry, a key-value pair like the information
    /// extracted from a single meta tag.
    /// </summary>
    [DebuggerDisplay( "name = {Name} value = {Value}" )]
    public class MetadataItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataItem"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        public MetadataItem( string name, string value )
        {
            if ( name == null )
            {
                throw new ArgumentNullException( "name" );
            }

            if ( value == null )
            {
                throw new ArgumentNullException( "value" );
            }

            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets the name of the property without the namespace prefix.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value of the property.
        /// </summary>
        public string Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Implements the operator ==. Returns true if the specified
        /// <see cref="MetadataItem" /> instances are equal or if both are
        /// null; otherwise, false.
        /// </summary>
        /// <param name="item">The x.</param>
        /// <param name="otherItem">The y.</param>
        /// <returns>
        /// True if the specified <see cref="MetadataItem" /> instances are
        /// equal or both are null; otherwise, false.
        /// </returns>
        public static bool operator ==(
            MetadataItem item,
            MetadataItem otherItem )
        {
            if ( ReferenceEquals( item, otherItem ) )
            {
                return true;
            }

            if ( ReferenceEquals( item, null ) ||
                ReferenceEquals( otherItem, null ) )
            {
                return false;
            }

            return item.Equals( otherItem );
        }

        /// <summary>
        /// Implements the operator !=. Returns true if the specified
        /// <see cref="MetadataItem" /> instances are NOT equal; otherwise,
        /// false. The result of this method is equivalent to !(x==y).
        /// </summary>
        /// <param name="item">The x.</param>
        /// <param name="otherItem">The y.</param>
        /// <returns>
        /// True if the specified <see cref="MetadataItem" /> instances are NOT
        /// equal; otherwise, false.
        /// </returns>
        public static bool operator !=(
            MetadataItem item,
            MetadataItem otherItem )
        {
            return !( item == otherItem );
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is
        /// equal to this instance.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="System.Object"/> to compare with this instance.
        /// </param>
        /// <returns>
        /// True if the specified <see cref="System.Object"/> is equal to this
        /// instance; otherwise, false.
        /// </returns>
        public override bool Equals( object obj )
        {
            var other = obj as MetadataItem;
            return this.Equals( other );
        }

        /// <summary>
        /// Determines whether the specified <see cref="MetadataItem"/> is equal
        /// to this instance.
        /// </summary>
        /// <param name="other">
        /// The <see cref="MetadataItem"/> to compare with this instance.
        /// </param>
        /// <returns>
        /// True if the specified <see cref="MetadataItem" /> is equal to this
        /// instance; otherwise, false.
        /// </returns>
        public bool Equals( MetadataItem other )
        {
            if ( ReferenceEquals( other, null ) )
            {
                return false;
            }

            return this.Name == other.Name && this.Value == other.Value;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing
        /// algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^ this.Value.GetHashCode();
        }
    }
}
