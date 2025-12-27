import ui from '../../styles/primitives.module.css'
import styles from './Navbar.module.css'

const navLinks = [
  { label: 'Home', href: '#' },
  { label: 'Discover', href: '#' },
  { label: 'Lists', href: '#' },
  { label: 'Calendar', href: '#' },
]

function Navbar() {
  return (
    <header className={styles.shell}>
      <div className={styles.scanline} aria-hidden />
      <nav className={styles.navbar}>
        <div className={styles.brand}>
          <div className={styles.brandChip}>YA</div>
          <div className={styles.brandText}>
            <span className={styles.brandStrong}>YetAnother</span>
            <span className={styles.brandGlow}>AnimeList</span>
          </div>
        </div>

        <ul className={styles.links}>
          {navLinks.map((link) => (
            <li key={link.label}>
              <a className={styles.link} href={link.href}>
                {link.label}
              </a>
            </li>
          ))}
        </ul>

        <div className={styles.searchArea}>
          <input
            className={styles.search}
            type="search"
            name="search"
            placeholder="Search anime..."
            aria-label="Search anime"
          />
        </div>

        <div className={styles.actions}>
          <button className={`${ui.button} ${ui.pill} ${ui.ghost}`}>Random</button>
          <button className={`${ui.button} ${ui.pill} ${ui.primary}`}>
            Add title
          </button>
        </div>
      </nav>
    </header>
  )
}

export default Navbar
