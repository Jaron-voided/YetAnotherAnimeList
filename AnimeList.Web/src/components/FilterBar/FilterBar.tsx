import { useEffect, useState } from 'react'
import ui from '../../styles/primitives.module.css'
import styles from './FilterBar.module.css'

export type Filters = {
  type: string
  rating: string
  status: string
  sort: string
}

type Props = {
  value?: Filters
  onChange?: (filters: Filters) => void
  onApply?: (filters: Filters) => void
}

const defaultFilters: Filters = {
  type: 'Any',
  rating: 'All',
  status: 'Any',
  sort: 'Trending',
}

const typeOptions = [
  { value: 'Any', label: 'Any type' },
  { value: 'Movie', label: 'Movie' },
  { value: 'Music', label: 'Music' },
  { value: 'OVA', label: 'OVA' },
  { value: 'Special', label: 'Special' },
  { value: 'TV', label: 'TV' },
  { value: 'ONA', label: 'ONA' },
  { value: 'CM', label: 'CM' },
  { value: 'TVSpecial', label: 'TV Special' },
  { value: 'PV', label: 'PV' },
  { value: 'Unknown', label: 'Unknown' },
]

const ratingOptions = ['All', '9+', '8+', '7+', '6+', '5+']
const statusOptions = ['Any', 'Airing', 'Completed', 'Upcoming']
const sortOptions = ['Trending', 'Rating', 'Most Popular', 'A-Z', 'Newest']

function FilterBar({ value, onChange, onApply }: Props) {
  const [internalFilters, setInternalFilters] = useState<Filters>(
    value ?? defaultFilters,
  )

  useEffect(() => {
    if (value) {
      setInternalFilters(value)
    }
  }, [value])

  const filters = value ?? internalFilters

  const update = (key: keyof Filters, next: string) => {
    if (filters[key] === next) return
    const updated = { ...filters, [key]: next }
    if (!value) {
      setInternalFilters(updated)
    }
    onChange?.(updated)
  }

  return (
    <section className={styles.shell} aria-label="Filter and sort anime">
      <div className={styles.row}>
        <div className={styles.group}>
          <p className={styles.label}>Rating</p>
          <div className={styles.chips}>
            {ratingOptions.map((option) => (
              <button
                key={option}
                className={`${ui.button} ${ui.pill} ${styles.chip} ${
                  filters.rating === option ? styles.chipActive : ''
                }`}
                onClick={() => update('rating', option)}
                type="button"
              >
                {option}
              </button>
            ))}
          </div>
        </div>

        <div className={styles.group}>
          <p className={styles.label}>Status</p>
          <div className={styles.chips}>
            {statusOptions.map((option) => (
              <button
                key={option}
                className={`${ui.button} ${ui.pill} ${styles.chip} ${
                  filters.status === option ? styles.chipActive : ''
                }`}
                onClick={() => update('status', option)}
                type="button"
              >
                {option}
              </button>
            ))}
          </div>
        </div>
      </div>

      <div className={styles.rowControls}>
        <div className={styles.group}>
          <p className={styles.label}>Sort</p>
          <div className={styles.selectShell}>
            <select
              value={filters.sort}
              onChange={(event) => update('sort', event.target.value)}
              className={styles.select}
            >
              {sortOptions.map((option) => (
                <option key={option} value={option}>
                  {option}
                </option>
              ))}
            </select>
          </div>
        </div>

        <div className={styles.group}>
          <p className={styles.label}>Type</p>
          <div className={styles.selectShell}>
            <select
              value={filters.type}
              onChange={(event) => update('type', event.target.value)}
              className={styles.select}
            >
              {typeOptions.map((option) => (
                <option key={option.value} value={option.value}>
                  {option.label}
                </option>
              ))}
            </select>
          </div>
        </div>

        <div className={styles.actions}>
          <button
            type="button"
            className={`${ui.button} ${ui.pill} ${ui.primaryBlue}`}
            onClick={() => onApply?.(filters)}
          >
            Apply filters
          </button>
        </div>
      </div>
    </section>
  )
}

export default FilterBar
