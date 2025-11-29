import { useEffect, useState } from 'react'
import styles from './App.module.css'
import ui from './styles/primitives.module.css'
import Navbar from './components/Navbar'
import FilterBar, { type Filters } from './components/FilterBar'
import { type Anime } from './models'
import { fetchAllAnime } from './services/animeApi'

const spotlight = [
  {
    title: 'Top Movies',
    items: ['Cyber City Oedo 808', 'Akira', 'Psycho-Pass'],
  },
  {
    title: 'Top Series (TV)',
    items: ['Ninja Kamui', 'Solo Leveling', 'Frieren'],
  },
  {
    title: 'Top Finished Airing',
    items: ['Cowboy Bebop', 'Trigun Stampede', 'Bubblegum Crisis'],
  },
]

const initialFilters: Filters = {
  type: 'Any',
  rating: 'All',
  status: 'Any',
  sort: 'Trending',
}

function App() {
  const [filters, setFilters] = useState<Filters>(initialFilters)
  const [featured, setFeatured] = useState<Anime[]>([])
  const [animeList, setAnimeList] = useState<Anime[]>([])
  const [filteredList, setFilteredList] = useState<Anime[]>([])
  const [hasApplied, setHasApplied] = useState(false)

  useEffect(() => {
    const load = async () => {
      try {
        const anime = await fetchAllAnime()
        if (!anime.length) return
        // Pick first two for MVP; can randomize later
        setFeatured(anime.slice(0, 2))
        setAnimeList(anime)
      } catch (error) {
        console.error('Failed to load featured anime', error)
      }
    }

    void load()
  }, [])

  const applyFilters = (nextFilters: Filters) => {
    const filtered = animeList
      .filter((anime) => {
        const matchesType =
          nextFilters.type === 'Any' || anime.type === nextFilters.type

        const matchesStatus =
          nextFilters.status === 'Any' || anime.status === nextFilters.status

        const ratingThreshold = (() => {
          if (nextFilters.rating === 'All') return null
          const value = Number(nextFilters.rating.replace('+', ''))
          return Number.isNaN(value) ? null : value
        })()

        const matchesRating =
          ratingThreshold === null || (anime.score ?? 0) >= ratingThreshold

        return matchesType && matchesStatus && matchesRating
      })
      .sort((a, b) => {
        switch (nextFilters.sort) {
          case 'Rating':
            return (b.score ?? 0) - (a.score ?? 0)
          case 'Most Popular':
            return (
              (a.popularity ?? Number.MAX_SAFE_INTEGER) -
              (b.popularity ?? Number.MAX_SAFE_INTEGER)
            )
          case 'A-Z':
            return a.title.localeCompare(b.title)
          case 'Newest':
            return (b.year ?? 0) - (a.year ?? 0)
          default:
            return 0
        }
      })

    setFilteredList(filtered.slice(0, 8)) // cap to ~2 rows while testing
    setFilters(nextFilters)
    setHasApplied(true)
  }

  return (
    <div className={styles.appShell}>
      <Navbar />

      <main className={styles.content}>
        <section className={styles.hero}>
          <div className={styles.heroGrid}>
            <div>
              <h1 className={styles.title}>
                <span className={styles.blue}>YetAnother</span>
                <br />
                <span className={styles.glow}>AnimeList</span>
              </h1>
              <div className={`${ui.actions} ${styles.actions} ${styles.heroActions}`}>
                <button className={`${ui.button} ${ui.primary}`}>Continue watching</button>
                <button className={`${ui.button} ${ui.ghost}`}>Open lists</button>
              </div>
            </div>

            <div className={styles.posterShell}>
              {[0, 1].map((index) => {
                const anime = featured[index]
                return (
                  <div className={styles.posterCard} key={index}>
                    <div
                      className={`${styles.posterBox} ${
                        anime?.imageUrl ? styles.posterHasImage : ''
                      }`}
                      style={
                        anime?.imageUrl
                          ? { backgroundImage: `url(${anime.imageUrl})` }
                          : undefined
                      }
                    >
                      {!anime?.imageUrl && 'Artwork'}
                    </div>
                    <p className={styles.posterCaption}>
                      {anime?.title ?? (index === 0 ? 'Feature title goes here' : 'Secondary feature')}
                    </p>
                  </div>
                )
              })}
            </div>
          </div>
        </section>

        <section className={styles.spotlight}>
          {spotlight.map((group) => (
            <div className={ui.panel} key={group.title}>
              <header className={ui.panelHeader}>
                <div className={ui.signal} />
                <p className={ui.panelLabel}>{group.title}</p>
              </header>
              <ul className={styles.panelList}>
                {group.items.map((item) => (
                  <li className={styles.panelItem} key={item}>
                    {item}
                  </li>
                ))}
              </ul>
            </div>
          ))}
        </section>

        <FilterBar value={filters} onChange={setFilters} onApply={applyFilters} />

        {hasApplied && (
          <section className={styles.results}>
            <header className={styles.resultsHeader}>
              <p className={styles.resultsLabel}>Results</p>
              <p className={styles.resultsCount}>
                Showing {filteredList.length} of {animeList.length} title
                {animeList.length === 1 ? '' : 's'}
              </p>
            </header>

            <div className={styles.resultsGrid}>
              {filteredList.map((anime) => (
                <article className={styles.resultCard} key={anime.id}>
                  <div
                    className={`${styles.resultPoster} ${
                      anime.imageUrl ? styles.posterHasImage : ''
                    }`}
                    style={
                      anime.imageUrl
                        ? { backgroundImage: `url(${anime.imageUrl})` }
                        : undefined
                    }
                  >
                    {!anime.imageUrl && 'No image'}
                  </div>
                  <div className={styles.resultMeta}>
                    <p className={styles.resultTitle}>{anime.title}</p>
                    <p className={styles.resultSub}>
                      {anime.type} • Score: {anime.score ?? 'N/A'}
                    </p>
                  </div>
                </article>
              ))}
              {!filteredList.length && (
                <div className={styles.noResults}>No titles match these filters</div>
              )}
            </div>
          </section>
        )}
      </main>
    </div>
  )
}

export default App
