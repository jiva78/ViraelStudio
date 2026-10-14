
import { useState } from 'react'
import './App.css'

const products = [
    {
        id: 1,
        name: 'Брошка «Queen»',
        type: 'Брошки',
        price: 1100,
    },
    {
        id: 2,
        name: 'Кленовий листок',
        type: 'Брошки',
        price: 1200,
    },
    {
        id: 3,
        name: 'Сережки «Мак»',
        type: 'Сережки',
        price: 950,
    },
    {
        id: 4,
        name: 'Кавунові сережки',
        type: 'Сережки',
        price: 1040,
    },
]

const categories = ['Усі', 'Брошки', 'Сережки', 'Кольє']

function App() {
    const [activeCategory, setActiveCategory] = useState('Усі')

    const visibleProducts =
        activeCategory === 'Усі'
            ? products
            : products.filter((product) => product.type === activeCategory)

    return (
        <main className="shop">
            <header className="shop-header">

                <a className="logo" href="#home" aria-label="Virael Studio — на головну">
                    <span className="logo-name">Virael Studio</span>
                </a>




                <nav className="category-nav" aria-label="Категорії прикрас">
                    {categories.map((category) => (
                        <button
                            key={category}
                            className={`category-button ${activeCategory === category ? 'active' : ''
                                }`}
                            onClick={() => setActiveCategory(category)}
                            aria-pressed={activeCategory === category}
                        >
                            {category}
                        </button>
                    ))}
                </nav>
            </header>

            <section className="product-grid" id="home" aria-label="Прикраси">
                {visibleProducts.map((product) => (
                    <article className="product-card" key={product.id}>
                        <a
                            className="product-image"
                            href="#"
                            aria-label={`Переглянути ${product.name}`}
                            onClick={(event) => event.preventDefault()}
                        >
                            <span className="image-placeholder">Фото додамо</span>
                        </a>

                        <div className="product-info">
                            <h2>{product.name}</h2>
                            <p>{product.price.toLocaleString('uk-UA')} ₴</p>
                        </div>
                    </article>
                ))}
            </section>


            <footer className="shop-footer">

                <a className="footer-brand" href="#home" aria-label="Virael Studio — на головну">
                    <img
                        className="logo-image"
                        src="/images/virael-studio-logo.png"
                        alt="Virael Studio"
                    />
                </a>


                <nav className="footer-links" aria-label="Інформація">
                    <a href="/delivery">Доставка й оплата</a>
                    <a href="/returns">Обмін і повернення</a>
                    <a href="/contacts">Контакти</a>
                    <a href="/privacy">Конфіденційність</a>
                </nav>

                <span className="footer-copyright">
                    © {new Date().getFullYear()} ViraelStudio
                </span>
            </footer>

        </main>
    )
}

export default App
